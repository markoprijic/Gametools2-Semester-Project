using UnityEngine;

public class enemy_Full_Death_Trigger : MonoBehaviour
{
    [SerializeField] Enemy_Movement movement;
    
    public void Finish_Death_Anim()
    {
        movement.has_Died = true;
    }
}
