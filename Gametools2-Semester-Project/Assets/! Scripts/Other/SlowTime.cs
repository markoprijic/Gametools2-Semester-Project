using UnityEngine;

public class SlowTime : MonoBehaviour
{
    [Header("Time Slow Settings")]
    [Tooltip("Factor by which time is slowed (e.g., 0.5 is half-speed).")]
    public float slowdownFactor = 0.5f;

    [Tooltip("Duration for which time is slowed (in seconds).")]
    public float slowdownDuration = 5f;

    private bool isSlowingTime = false;
    private float slowdownEndTime;

    private void Start()
    {
        //eMovement_Script = GameObject.FindGameObjectWithTag("Player").GetComponent<Gun_Controller>();
        //eShoot_Script = GameObject.FindGameObjectWithTag("Player").GetComponent<Gun_Controller>();
    }
    void Update()
    {
        // Check if the button to slow time is pressed and not already slowing time
        if (Input.GetKeyDown(KeyCode.Q) && !isSlowingTime)
        {
            StartSlowTime();
        }

        // Check if the slowdown duration has passed
        if (isSlowingTime && Time.unscaledTime >= slowdownEndTime)
        {
            ResetTime();
        }
    }

    void StartSlowTime()
    {
        // Set timeScale to the slowdown factor
        Time.timeScale = slowdownFactor;
        // Adjust fixedDeltaTime to maintain consistent physics calculations
        Time.fixedDeltaTime = Time.fixedDeltaTime * slowdownFactor;

        isSlowingTime = true;
        slowdownEndTime = Time.unscaledTime + slowdownDuration;
    }

    void ResetTime()
    {
        // Restore timeScale to normal
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f; // Default fixedDeltaTime value

        isSlowingTime = false;
    }
}
