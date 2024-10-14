using System;
using UnityEngine;

public class Gun_Controller : MonoBehaviour
{
    [Header("General")] 
    [SerializeField] private Camera camera;
    public bool can_Fire;

    [SerializeField] private Teleport_Pistol tp_Pistol_Script;
    private bool tp_Pistol_Equipped;
    public bool tp_Pistol_Available; // if tp_Pistol is useable in current level, for future
    
    // not locking items out completely, just not setting equip bool to true
    // shouldnt cause issues on levels where you dont have that weapon but who knows
    

    private void Start()
    {
        tp_Pistol_Equipped = true;
        can_Fire = true;
    }

    public void Shoot()
    {
        if (tp_Pistol_Equipped && can_Fire)
        {
               tp_Pistol_Script.Shoot();
        }
    }

}
