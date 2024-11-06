using System;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.UIElements;

public class Kill_Counter : MonoBehaviour
{
    [SerializeField] private Lvl1_Victory victory_Script;
    
    private int total_Enemies = 0;
    [HideInInspector] public int total_Kills = 0;
    
    
    private void Start()
    {
        Find_Enemies();
    }// end Start()

    private void Update()
    {
        print(total_Enemies);
        print(total_Kills);
        
        if (total_Kills >= total_Enemies)
        {
            victory_Script.Victory_Condition_Met();
        }
    }
    
    private void Find_Enemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        foreach (GameObject enemy in enemies)
        {
            total_Enemies++;
        }
    }// end Find_Enemies()
    
}// end script
