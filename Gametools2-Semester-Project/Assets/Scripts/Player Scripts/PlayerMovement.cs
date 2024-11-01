using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement variables
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float dashSpeed = 10f;
    public float wallRunSpeed = 7f;
    public float dashTime = 0.2f;
    public float gravityMultiplier = 2f;
    public float lookSensitivity = 100f;

    // Jumping and wall running variables
    private bool canDoubleJump;
    private bool isWallRunning;
    private bool isDashing;

    // Wall running variables
    public float wallRunDuration = 1.5f;
    private float wallRunTimer;

    // Components
    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 moveDirection;
    public bool isFacingWall;
    private Vector3 wallNormal;

    // Camera variables for looking around
    public Transform playerCamera;
    private float xRotation = 0f;

    // Layer for walls
    public LayerMask wallLayer;

    // Dash cooldown
    private bool canDash = true;
    private float dashCooldown = 1.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // Hide and lock the cursor for look-around
    }

    void Update()
    {
        // Mouse look (camera rotation)
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limit vertical rotation

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Vertical rotation (pitch)
        transform.Rotate(Vector3.up * mouseX); // Horizontal rotation (yaw)

        // Basic movement input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        moveDirection = transform.right * moveX + transform.forward * moveZ;

        // Ground movement and jumping logic
        if (isGrounded && !isWallRunning)
        {
            MovePlayer();

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
        else
        {
            MovePlayer(); // Allow movement in the air
        }

        if (!isGrounded && canDoubleJump && !isWallRunning)
        {
            if (Input.GetButtonDown("Jump"))
            {
                DoubleJump();
            }
        }

        if (Input.GetButtonDown("Dash") && canDash && !isDashing)
        {
            StartCoroutine(Dash());
        }

        CheckForWallRun();

        if (isWallRunning)
        {
            WallRun();
            if (Input.GetButtonDown("Jump"))
            {
                JumpOffWall();
            }
        }

        ApplyGravity();
    }

    void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
        if (isGrounded)
        {
            canDoubleJump = true;
        }
    }

    private void MovePlayer()
    {
        // Move the player regardless of whether they're grounded or airborne
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
    }

    private void DoubleJump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        canDoubleJump = false;
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        float startTime = Time.time;

        Vector3 dashDirection = moveDirection * dashSpeed;

        while (Time.time < startTime + dashTime)
        {
            rb.linearVelocity = new Vector3(dashDirection.x, rb.linearVelocity.y, dashDirection.z);
            yield return null;
        }

        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void CheckForWallRun()
    {
        RaycastHit hit;
        // Check both sides for walls to enable wall run
        if (Physics.Raycast(transform.position, transform.right, out hit, 1f, wallLayer) ||
            Physics.Raycast(transform.position, -transform.right, out hit, 1f, wallLayer))
        {
            isFacingWall = true;
            wallNormal = hit.normal;

            if (Input.GetButton("Jump") && !isGrounded)
            {
                StartWallRun();
            }
        }
        else
        {
            isFacingWall = false;
            StopWallRun();
        }
    }

    private void StartWallRun()
    {
        isWallRunning = true;
        wallRunTimer = wallRunDuration;
        rb.useGravity = false;

        // Adjust the player's orientation slightly to align with the wall for a better run
        Vector3 wallRunDirection = Vector3.Cross(Vector3.up, wallNormal).normalized;
        moveDirection = wallRunDirection;
    }

    private void WallRun()
    {
        rb.linearVelocity = new Vector3(moveDirection.x * wallRunSpeed, 0, moveDirection.z * wallRunSpeed);
        wallRunTimer -= Time.deltaTime;

        if (wallRunTimer <= 0)
        {
            StopWallRun();
        }
    }

    private void JumpOffWall()
    {
        Vector3 wallJumpDirection = transform.forward + wallNormal;
        rb.linearVelocity = new Vector3(wallJumpDirection.x * moveSpeed, jumpForce, wallJumpDirection.z * moveSpeed);
        StopWallRun();
    }

    private void StopWallRun()
    {
        isWallRunning = false;
        rb.useGravity = true;
    }

    private void ApplyGravity()
    {
        if (!isGrounded && !isWallRunning)
        {
            rb.AddForce(Vector3.down * gravityMultiplier);
        }
    }
}
