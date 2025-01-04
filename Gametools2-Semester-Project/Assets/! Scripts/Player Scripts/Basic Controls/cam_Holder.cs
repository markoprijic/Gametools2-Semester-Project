using UnityEngine;

public class cam_Holder : MonoBehaviour
{
    public Transform camera_Position;

    void Update()
    {
        gameObject.transform.position = camera_Position.position;
    }
}
