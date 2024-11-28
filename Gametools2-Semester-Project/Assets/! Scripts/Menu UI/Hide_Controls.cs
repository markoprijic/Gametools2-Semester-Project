using UnityEngine;

public class Hide_Controls : MonoBehaviour
{
    [SerializeField] private GameObject controls;

    public void Hide_Control_Screen()
    {
        controls.SetActive(false);
    }
}
