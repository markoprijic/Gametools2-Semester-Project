using UnityEngine;
using System.Collections;

public class Enemy_Shooting : MonoBehaviour
{
    [Header("General")] 
    [SerializeField] private GameObject view_Point;

    [Header("Burst Rifle")] 
    [SerializeField] private GameObject burst_Rifle_Projectile_Prefab;
    [SerializeField] private Transform bullet_Spawn;
    [SerializeField] private float bullet_Force;
    [SerializeField] private float burst_Cooldown; // Time between shots within the burst, will be very short
    [SerializeField] private int bullets_Per_Burst; // Amount of shots fired in single burst
    
    
    public void Shoot()
    {
        print("shooting");
        StartCoroutine(Burst_Fire());
    }// end Shoot()

    private IEnumerator Burst_Fire()
    {
        for (int i = 0; i < bullets_Per_Burst; i++)
        {
            Shoot_Projectile();
            yield return new WaitForSeconds(burst_Cooldown);
        }
        
        Invoke("Reset_Fire", burst_Cooldown);
    }

    private void Shoot_Projectile()
    {
        // Find point to shoot projectile at
        Ray ray = new Ray(view_Point.transform.position, view_Point.transform.forward);
        RaycastHit hit;
        
        // Check if ray hits
        Vector3 target_Point;
        if (Physics.Raycast(ray, out hit))
            target_Point = hit.point;
        else
            target_Point = ray.GetPoint(50); // random value far away, for looking at sky etc
        
        // Calculate direction
        Vector3 projectile_Direction = target_Point - bullet_Spawn.position;
        
        GameObject projectile = Instantiate(burst_Rifle_Projectile_Prefab, bullet_Spawn.position, Quaternion.identity);
        projectile.transform.forward = projectile_Direction.normalized;
        projectile.GetComponent<Rigidbody>().AddForce(projectile_Direction.normalized * bullet_Force);
    }// end Shoot_Projectile()
    
}// end script