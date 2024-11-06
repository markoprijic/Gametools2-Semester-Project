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

    private void OnPrevious()
    {
        gun_Control_Script.current_Weapon_I--;
        if (gun_Control_Script.current_Weapon_I < 0)
            gun_Control_Script.current_Weapon_I = gun_Control_Script.total_Weapons;
        
        gun_Control_Script.Change_Weapon();
    }

    private void OnNext()
    {
        gun_Control_Script.current_Weapon_I++;
        if (gun_Control_Script.current_Weapon_I > gun_Control_Script.total_Weapons)
            gun_Control_Script.current_Weapon_I = 0;
        
        gun_Control_Script.Change_Weapon();
    }
    
    private void OnAttack()
    {
        gun_Control_Script.Shoot();
    }
}
