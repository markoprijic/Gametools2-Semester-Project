using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player_Status : MonoBehaviour, IEnemy_Burst_Damage
{

    [Header("General")]
    [SerializeField] private Damage_Controller damage_Control_Script;
    [SerializeField] Slider health_Bar;
    
    [Header("Player Health")]
    [SerializeField] private float health = 100;
    [SerializeField] private float regen_Rate;
    [SerializeField] private float regen_Delay;

    private void Update()
    {
        health_Bar.value = health * 0.01f; // value is 0 to 1
        
        if (health <= 0)
        {
            print("Player Dead");
        }
        else if (health > 100)
            health = 100;
        else if (health < 100 && health > 0)
            StartCoroutine(Health_Regen());
    }

    IEnumerator Health_Regen()
    {
        yield return new WaitForSeconds(regen_Delay);

        if (health < 100)
        {
            health += regen_Rate * Time.deltaTime;
        }
        
    }// end Health_Regen()
    
    #region --- Incoming Damage ---
    
    public void Recieve_Enemy_Burst_Rifle_Damage()
    {
        health -= damage_Control_Script.enemy_Burst_Rifle_Damage;
        StopAllCoroutines();
    }// end Recieve_Enemy_Burst_Rifle_Damage
    
    #endregion
    
}// end script
