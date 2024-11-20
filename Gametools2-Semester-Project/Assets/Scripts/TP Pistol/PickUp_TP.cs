using UnityEngine;

public class PickUp_TP : MonoBehaviour, IInteractable
{
    private Gun_Controller gun_Control_Script;

    
    private void Start()
    {
        gun_Control_Script = GameObject.FindGameObjectWithTag("Player").GetComponent<Gun_Controller>();
    }// end Start()

    public void Interact()
    {
        gun_Control_Script.tp_Pistol_Available = true;
        gun_Control_Script.total_Weapons++;
        gun_Control_Script.current_Weapon_I = 3;
        gun_Control_Script.Change_Weapon();
        
        Destroy(gameObject);
    }// end Interat()
    
}// end script
