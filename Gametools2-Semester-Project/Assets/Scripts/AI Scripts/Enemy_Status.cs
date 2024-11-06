using System;
using UnityEngine;

public class Enemy_Status : MonoBehaviour, IBurst_Rifle_Damage, IPlasma_Projectile, ITP_Pistol_Damage
{
    private Damage_Controller damage_Control_Script;

    [Header("General")] 
    [SerializeField] private Enemy_Movement movement_Script;
    [SerializeField] private int total_HP;
    public int current_HP;
    
    private void Start()
    {
        GameObject controller = GameObject.FindGameObjectWithTag("Damage Controller");
        damage_Control_Script = controller.GetComponent<Damage_Controller>();
        current_HP = total_HP;
    }

    private void Update()
    {
        if (current_HP <= 0)
        {
            movement_Script.Change_State(6);
        }
    }
    
    private void Take_Damage(int incoming_Damage)
    {
        current_HP -= incoming_Damage;
        movement_Script.Change_State(4); // attack player on being damaged
        //Debug.Log($"Damage recieved, current health: {current_HP}");
    }
    
    #region - Damage Types -

    public void Recieve_Burst_Rifle_Damage()
    {
        Take_Damage(damage_Control_Script.burst_Rifle_Damage);
    }
    
    public void Recieve_Plasma_Damage()
    {
        Take_Damage(damage_Control_Script.plasma_Gun_Damage);
    }
    public void Receive_TP_Pistol_Damage()
    {
        Take_Damage(damage_Control_Script.tp_Pistol_Damage);
    }
    
    #endregion
    
}
