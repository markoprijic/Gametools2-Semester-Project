using System;
using UnityEngine;

public class Lvl1_End_Trigger : MonoBehaviour
{
    [SerializeField] private Victory_Manager victory_Script;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            victory_Script.Next_Scene();
        }
    }
}
