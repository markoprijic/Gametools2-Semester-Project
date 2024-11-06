using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Status : MonoBehaviour, IEnemy_Burst_Damage
{

    [Header("General")]
    [SerializeField] private Damage_Controller damage_Control_Script;
    [SerializeField] Slider health_Bar;
    
    [Header("Player Health")]
    [SerializeField] private float health = 100;
    [SerializeField] private float regen_Rate;
    [SerializeField] private float regen_Delay;

    [Header("Scene Transition")] 
    [SerializeField] private Material black_Screen;
    [Range(0, 1)] 
    [SerializeField] private float fade_Speed;
    [SerializeField] private bool start_Sequence;
    [SerializeField] private bool death_Sequence;

    private Color fade_Transparancy;
    
    
    private void Start()
    {
        Color reset_Colour = black_Screen.color;
        reset_Colour.a = 1;
        black_Screen.color = reset_Colour;
        
        start_Sequence = true;
        death_Sequence = false;
        fade_Transparancy = black_Screen.color;
    }

    private void Update()
    {
        Fading_Screen();
        
        health_Bar.value = health * 0.01f; // value is 0 to 1
        
        if (health <= 0)
        {
            Player_Death();
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

    private void Player_Death()
    {
        death_Sequence = true;
    }// end Player_Death()

    private void Fading_Screen()
    {
        if (start_Sequence == true && black_Screen.color.a >= 0)
        {
            fade_Transparancy.a -= fade_Speed * Time.deltaTime;
            black_Screen.color = fade_Transparancy;
            
            if (black_Screen.color.a == 0)
            {
                start_Sequence = false;
            }
        }

        if (death_Sequence == true)
        {
            start_Sequence = false;
            //print("buh");
            if (black_Screen.color.a <= 1)
            {
                //print("ah");

                fade_Transparancy.a += fade_Speed * Time.deltaTime;
                black_Screen.color = fade_Transparancy;

                if (black_Screen.color.a >= 1)
                {
                    death_Sequence = false;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
    }// end Fading_Screen()
    
    #region --- Incoming Damage ---
    
    public void Recieve_Enemy_Burst_Rifle_Damage()
    {
        health -= damage_Control_Script.enemy_Burst_Rifle_Damage;
        StopAllCoroutines();
    }// end Recieve_Enemy_Burst_Rifle_Damage
    
    #endregion
    
}// end script
