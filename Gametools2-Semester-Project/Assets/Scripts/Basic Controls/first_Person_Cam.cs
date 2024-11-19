using System;
using UnityEngine;

public class first_Person_Cam : MonoBehaviour
{
    public Transform orientation;
    public float sens_X;
    public float sens_Y;
    
    private float x_Rotation;
    private float y_Rotation;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // get mouse input
        float mouse_X = Input.GetAxis("Mouse X") * Time.deltaTime * sens_X;
        float mouse_Y = Input.GetAxis("Mouse Y") * Time.deltaTime * sens_Y;

        y_Rotation += mouse_X;
        x_Rotation -= mouse_Y;
        x_Rotation = Mathf.Clamp(x_Rotation, -90f, 90f);
        
        // rotate camera and orientation
        transform.rotation = Quaternion.Euler(x_Rotation, y_Rotation, 0f);
        orientation.localRotation = Quaternion.Euler(0, y_Rotation, 0);

    }
}
