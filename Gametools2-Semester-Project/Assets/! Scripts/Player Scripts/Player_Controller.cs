using System;
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

    private void OnItem_1()
    {
        gun_Control_Script.current_Weapon_I = 0;
        gun_Control_Script.Change_Weapon();
    }

    private void OnItem_2()
    {
        if (gun_Control_Script.plasma_Gun_Available == false) return;
        gun_Control_Script.current_Weapon_I = 2;
        gun_Control_Script.Change_Weapon();
    }

    private void OnItem_3()
    {
        if (gun_Control_Script.tp_Pistol_Available == false) return;
        gun_Control_Script.current_Weapon_I = 3;
        gun_Control_Script.Change_Weapon();
    }
    
    private void OnAttack()
    {
        gun_Control_Script.Shoot();
    }
}
