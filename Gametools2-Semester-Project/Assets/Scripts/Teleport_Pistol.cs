using UnityEngine;

public class Teleport_Pistol : MonoBehaviour
{
    [Header("Teleport Pistol")] 
    [SerializeField] private GameObject tp_Pistol;
    [SerializeField] private GameObject tp_Pistol_Projectile;
    [SerializeField] private Transform tp_Pistol_Bullet_Spawn;
    [SerializeField] private float tp_Pistol_Bullet_Force;
    [SerializeField] private float tp_Pistol_Cooldown;

    public void Shoot()
    {
        print("shoot");
    }
}
