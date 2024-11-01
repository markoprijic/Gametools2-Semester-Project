using System;
using UnityEngine;

public class Player_Status : MonoBehaviour, IEnemy_Burst_Damage
{

    [Header("General")]
    [SerializeField] private Damage_Controller damage_Control_Script;

    [Header("Player Health")]
    [SerializeField] private int health = 100;

    private void Update()
    {
        
        if (health <= 0)
        {
            print("Player Dead");
        }
    }


    public void Recieve_Enemy_Burst_Rifle_Damage()
    {
        health -= damage_Control_Script.enemy_Burst_Rifle_Damage;
        print(health);
    }// end Recieve_Enemy_Burst_Rifle_Damage
    
}// end script
