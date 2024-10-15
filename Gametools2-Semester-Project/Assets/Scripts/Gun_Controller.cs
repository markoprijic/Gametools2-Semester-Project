using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class Gun_Controller : MonoBehaviour
{
    [Header("General")] 
    [SerializeField] private Camera camera;
    
    public int total_Weapons;
    [SerializeField] private int[] current_Weapon_Roster; // array for switching  
    public int current_Weapon_I = 0; // index for current weapon
    
    public bool can_Fire;
    
    
    // not locking items out completely, just not setting equip bool to true
    // shouldnt cause issues on levels where you dont have that weapon but who knows
    
    // weapon_Avaiable bools for possible later use in level control scripts, may use something else
    
    [Header("TP Pistol")]
    [SerializeField] private GameObject tp_Pistol;
    [SerializeField] private Teleport_Pistol tp_Pistol_Script;
    public bool tp_Pistol_Available; 

    [Header("Plasma Gun")] 
    [SerializeField] private GameObject plasma_Gun;
    [SerializeField] private Plasma_Gun plasma_Gun_Script;
    public bool plasma_Gun_Available;

    private void Start()
    {
        current_Weapon_Roster = new int[total_Weapons];
    
        can_Fire = true;
        Change_Weapon();
    }// end Start()

    public void Change_Weapon()
    {
        switch (current_Weapon_I)
        {
            case 0:
                can_Fire = true;
                tp_Pistol.SetActive(true);
                plasma_Gun.SetActive(false);
                break;
            
            case 1:
                can_Fire = true;
                plasma_Gun.SetActive(true);
                tp_Pistol.SetActive(false);
                break;
                
        } // end switch (current_Weapon_I)
    }// end Change_Weapon()
    
    public void Shoot()
    {
        if (tp_Pistol.activeInHierarchy && can_Fire)
               tp_Pistol_Script.Shoot();
        
        else if (plasma_Gun.activeInHierarchy && can_Fire)
            plasma_Gun_Script.Shoot();
    }// end Shoot()

}
