using UnityEngine;
using System;
using System.Collections;
using Unity.VisualScripting;

public class medkit : MonoBehaviour
{
    private Player_Status playerStatusScript;

    public float healAmount = 30;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStatusScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Status>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    private void OnTriggerEnter(Collider other)
        {
            // Check if the object entering the trigger has the specified tag
            if (other.CompareTag("Player"))
            {
                if (playerStatusScript.health < 100)
                {
                    playerStatusScript.health += healAmount;
                    Destroy(gameObject);
                }
                
                
            }

        }
}
