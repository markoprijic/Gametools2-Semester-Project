using System;
using UnityEngine;

public class enemy_Full_Death_Trigger : MonoBehaviour
{
    [SerializeField] Enemy_Movement movement;
    private Kill_Counter kill_counter;
    private bool stop_Dying = false; // to stop looping

    private void Start()
    {
        GameObject controller = GameObject.FindGameObjectWithTag("Kill Counter");
        kill_counter = controller.GetComponent<Kill_Counter>();
    }

    public void Finish_Death_Anim()
    {
        if (stop_Dying == false)
        {
            stop_Dying = true;
            movement.has_Died = true;
            kill_counter.total_Kills++;
        }

    }
}
