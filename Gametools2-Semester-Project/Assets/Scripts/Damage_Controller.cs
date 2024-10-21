using UnityEngine;

/*
 * This is for housing different damage values
 * Easily adjustable in one place, saves from having to go in and out of all weapons
 * If we add buffs, they will be calculated here
 */

public class Damage_Controller : MonoBehaviour
{
    [Header("Player Weapon Damage")] 
    public int burst_Rifle_Damage;
    public int plasma_Gun_Damage;
    public int tp_Pistol_Damage;
    
}
