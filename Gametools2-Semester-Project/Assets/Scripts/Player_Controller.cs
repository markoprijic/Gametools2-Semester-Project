using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private Gun_Controller gun_Control_Script;
    private InputActionMap actionMap;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actionMap = new InputActionMap();
    }

    // Update is called once per frame


    private void OnAttack()
    {
        gun_Control_Script.Shoot();
    }
}
