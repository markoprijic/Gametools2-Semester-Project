using UnityEngine;

public class Teleport_Pistol : MonoBehaviour
{
    [Header("General")] 
    [SerializeField] private Gun_Controller gun_Control_Script;
    [SerializeField] private Camera camera;    
    
    [Header("Teleport Pistol")] 
    //[SerializeField] private GameObject tp_Pistol;
    [SerializeField] private GameObject tp_Pistol_Projectile_Prefab;
    [SerializeField] private GameObject tp_Point_Prefab; // Where the bullet must reach before tp starts
    [SerializeField] private Transform bullet_Spawn; // Point from which bullet spawns, calculated from barrel not camera
    [SerializeField] private float bullet_Force;
    [SerializeField] private float fire_Cooldown;
    [SerializeField] private float bullet_Range;

    
    public void Shoot()
    {
        print("shoot");
        
        // Find point to shoot projectile at
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        
        // Check if ray hits
        Vector3 target_Point;
        if (Physics.Raycast(ray, out hit, bullet_Range))
            target_Point = hit.point;
        else
            target_Point = ray.GetPoint(bullet_Range); // May not be necessary with bullet_Range in above if
        
        // Place tp_Point at end of ray
        GameObject tp_Point = Instantiate(tp_Point_Prefab, target_Point, Quaternion.identity);
        
        // Calculate direction
        Vector3 projectile_Direction = target_Point - bullet_Spawn.position;
    
        // Spawn and shoot projectile
        GameObject tp_Projectile = Instantiate(tp_Pistol_Projectile_Prefab, bullet_Spawn.position, Quaternion.identity);
        tp_Projectile.transform.forward = projectile_Direction.normalized;
        tp_Projectile.GetComponent<Rigidbody>().AddForce(projectile_Direction.normalized * bullet_Force);

        gun_Control_Script.can_Fire = false;
        Invoke("Reset_Fire", fire_Cooldown);
        // fire rate func

    }// end Shoot()

    private void Reset_Fire()
    {
        gun_Control_Script.can_Fire = true;
    }// end fire_Cooldown()

}// end script
