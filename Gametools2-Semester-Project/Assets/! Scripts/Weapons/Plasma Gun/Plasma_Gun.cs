using UnityEngine;

public class Plasma_Gun : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject player_Controller;
    [SerializeField] private Gun_Controller gun_Control_Script;
    [SerializeField] private Camera camera;

    [Header("Plasma Gun")] 
    [SerializeField] private GameObject plasma_Gun_Projectile_Prefab;
    [SerializeField] private Transform bullet_Spawn;
    [SerializeField] private float knockback_Force;
    [SerializeField] private float bullet_Force;
    [SerializeField] private float fire_Cooldown;
    
    
    public void Shoot()
    {
        // Find point to shoot projectile at
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        
        // Check if ray hits
        Vector3 target_Point;
        if (Physics.Raycast(ray, out hit))
            target_Point = hit.point;
        else
            target_Point = ray.GetPoint(50); // random value far away, for looking at sky etc
        
        // Calculate direction
        Vector3 projectile_Direction = target_Point - bullet_Spawn.position;
    
        // Spawn and shoot projectile
        GameObject projectile = Instantiate(plasma_Gun_Projectile_Prefab, bullet_Spawn.position, Quaternion.identity);
        projectile.transform.forward = projectile_Direction.normalized;
        projectile.GetComponent<Rigidbody>().AddForce(projectile_Direction.normalized * bullet_Force);

        // Knock player backwards
        player_Controller.GetComponent<Rigidbody>().AddForce(-camera.transform.forward * knockback_Force, ForceMode.Impulse);

        gun_Control_Script.can_Fire = false;
        Invoke("Reset_Fire", fire_Cooldown);
    }// end Shoot()
    
    private void Reset_Fire()
    {
        gun_Control_Script.can_Fire = true;
    }// end fire_Cooldown()
    
}// end script
