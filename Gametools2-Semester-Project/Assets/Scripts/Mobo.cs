using UnityEngine;

public class Mobo : MonoBehaviour, IInteractable
{
    [SerializeField] private Victory_Manager victory_Manager;

    public void Interact()
    {
        victory_Manager.Victory_Condition_Met();
    }
    
}
