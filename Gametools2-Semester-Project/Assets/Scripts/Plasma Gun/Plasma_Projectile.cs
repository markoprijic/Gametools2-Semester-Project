using System;
using UnityEngine;

interface IPlasma_Projectile // for use on enemy scripts to detect damage
{
    void Recieve_Plasma_Damage();
}

public class Plasma_Projectile : MonoBehaviour
{
    [SerializeField] GameObject projectile_GFX;
    [SerializeField] private LayerMask damageable_Layer; // enemy, player, possibly breakable objects
    [SerializeField] private int hit_Limit; // for explosion detection memory management
    [SerializeField] private float explosion_Radius;
    
    private Collider[] hit_Colliders; // for detecting what explosion has hit

    #region - basics -
    private void Awake()
    {
        hit_Colliders = new Collider[hit_Limit];
    }

    private void OnTriggerEnter(Collider other)
    {
        Explosion();
    }

    #endregion
    
    private void Explosion()
    {
        Destroy(projectile_GFX);
        // effect here
        Calculate_Hit_Targets();
    }// end Explosion()

    private void Calculate_Hit_Targets()
    {
        // gather targets within explosion_Radius on damageable_Layer
        int colliders_Hit_Count = Physics.OverlapSphereNonAlloc(transform.position,  explosion_Radius, hit_Colliders, damageable_Layer);

        // go through each object detected above and deal damage accordingly
        for (int i = 0; i < colliders_Hit_Count; i++)
        {
            Debug.Log($"Attempting to damage: {hit_Colliders[i].gameObject.name}");
            if (hit_Colliders[i].gameObject.CompareTag("Player"))
            {
                // damage the player
                IPlasma_Projectile player = hit_Colliders[i].gameObject.GetComponent<IPlasma_Projectile>();
                player.Recieve_Plasma_Damage();
            }
            else if (hit_Colliders[i].gameObject.CompareTag("Enemy"))
            {
                // damage the enemy
                IPlasma_Projectile enemy = hit_Colliders[i].gameObject.GetComponent<IPlasma_Projectile>();
                enemy.Recieve_Plasma_Damage();
            }
            
        }
        
    }// end Calculate_Hit_Targets()
    
}// end script
