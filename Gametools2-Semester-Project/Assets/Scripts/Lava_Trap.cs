using System;
using UnityEngine;

public class Lava_Trap : MonoBehaviour
{
    private Player_Status kill_Player;


    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        kill_Player = player.GetComponent<Player_Status>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            kill_Player.Player_Death();
    }
}
