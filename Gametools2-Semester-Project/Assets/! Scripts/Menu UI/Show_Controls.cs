using UnityEngine;

public class Show_Controls : MonoBehaviour
{
    [SerializeField] private GameObject control_Screen;

    public void Show_Control_Screen()
    {
        control_Screen.SetActive(true);
    }
}
