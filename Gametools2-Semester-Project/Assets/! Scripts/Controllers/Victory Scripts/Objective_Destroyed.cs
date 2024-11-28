using UnityEngine;

public class Objective_Destroyed : MonoBehaviour, IInteractable
{
    [SerializeField] private Lvl3_Victory victory_Trigger;

    public void Interact()
    {
        victory_Trigger.Objective_Destroyed();
    }
    
}
