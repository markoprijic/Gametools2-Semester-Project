using System;
using System.Collections;
using UnityEngine;

public class Enemy_Status : MonoBehaviour, IBurst_Rifle_Damage, IPlasma_Projectile, ITP_Pistol_Damage
{
    private Damage_Controller damage_Control_Script;

    [Header("General")] 
    [SerializeField] private Enemy_Movement movement_Script;
    [SerializeField] private int total_HP;
    
    private Collider collider;
    private SkinnedMeshRenderer[] renderers;
    private Color hit_Color = Color.green;
    private Color base_Color;
    
    public int current_HP;
    
    private void Start()
    {
        GameObject controller = GameObject.FindGameObjectWithTag("Damage Controller");
        damage_Control_Script = controller.GetComponent<Damage_Controller>();
        current_HP = total_HP;
        collider = GetComponent<Collider>();
        renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        base_Color = Color.white;
    }

    private void Update()
    {
        if (current_HP <= 0)
        {
            movement_Script.Change_State(6);
            collider.enabled = false;
        }
    }
    
    private void Take_Damage(int incoming_Damage)
    {
        current_HP -= incoming_Damage;
        
        StartCoroutine("Change_Colour");
        movement_Script.Change_State(4); // attack player on being damaged
        //Debug.Log($"Damage recieved, current health: {current_HP}");
    }

    private IEnumerator Change_Colour()
    {
        print("change colour");
        
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = hit_Color;
        }
        
        yield return new WaitForSeconds(0.7f);
        
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = base_Color;
        }
    }// end Reset_Colour

    private void Flash_Red()
    {
        
    }// end Flash_Red()
    
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
