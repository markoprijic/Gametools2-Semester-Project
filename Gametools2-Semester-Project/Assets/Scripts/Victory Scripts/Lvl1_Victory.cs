using UnityEngine;

public class Lvl1_Victory : MonoBehaviour
{
    [SerializeField] Player_Status playerStatus;
    [SerializeField] private GameObject final_Door;

    public void Victory_Condition_Met()
    {
        final_Door.SetActive(false);
    }

    public void Trigger_End()
    {
        // will be used for scene tranisition, now we're just killing the player
        playerStatus.Player_Death();
    }
    
}
