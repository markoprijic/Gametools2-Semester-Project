using System;
using UnityEngine;

interface IBurst_Rifle_Damage
{
    void Recieve_Burst_Rifle_Damage();
}

public class Burst_Rifle_Projectile : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter2D called. other's tag was {other.tag}.");

        if (other.tag == "Burst Projectile")
            return;
        
        else if (other.tag == "Enemy")
        {
            IBurst_Rifle_Damage enemy = other.gameObject.GetComponent<IBurst_Rifle_Damage>();
            enemy.Recieve_Burst_Rifle_Damage();
        }
        
        Destroy(gameObject);
    }
}// end script
