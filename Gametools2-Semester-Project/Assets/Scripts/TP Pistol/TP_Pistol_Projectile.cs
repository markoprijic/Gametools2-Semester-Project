using System;
using UnityEngine;

interface ITP_Pistol_Damage
{
    void Receive_TP_Pistol_Damage();
}

public class TP_Pistol_Projectile : MonoBehaviour
{
    [SerializeField] private GameObject player_Controller;
    private GameObject tp_Point;
    private void Awake()
    {
        player_Controller = GameObject.FindGameObjectWithTag("Player");
        tp_Point = GameObject.FindGameObjectWithTag("TP Point");
    }



    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter2D called. other's tag was {other.tag}.");
        if (other.tag == "Enemy") // In case an enemy/etc moves in the way of the bullet, blocking original path
        {
            ITP_Pistol_Damage enemy = other.gameObject.GetComponent<ITP_Pistol_Damage>();
            enemy.Receive_TP_Pistol_Damage();
            
            TP_Player();
            Destroy(tp_Point);
            Destroy(gameObject);
        }
        else if (other.tag == "TP Point")
        {
            TP_Player();
            Destroy(tp_Point);
            Destroy(gameObject);
        }
    }// end OnTriggerEnter()

    private void TP_Player()
    {
        player_Controller.transform.position = transform.position;    
    }// end TP_Player()
    
}// end script
