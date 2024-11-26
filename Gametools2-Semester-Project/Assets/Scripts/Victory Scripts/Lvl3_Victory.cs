using System;
using UnityEngine;

public class Lvl3_Victory : MonoBehaviour
{
    [SerializeField] private Victory_Manager victoryManager;
    [SerializeField] private GameObject final_Door;
    [HideInInspector] public int key_Total = 0;
    [HideInInspector] public bool total_Kills_Reached = false;
    
    private bool end_Door_Opened = false;

    private void Update()
    {
        if (key_Total == 2 && end_Door_Opened == true)
            end_Door_Opened = true;
        
        if (end_Door_Opened)
        {
            final_Door.SetActive(false);
        }
    }// end Update()

    public void Objective_Destroyed()
    {
        victoryManager.Next_Scene();
    }
    
}// end script
