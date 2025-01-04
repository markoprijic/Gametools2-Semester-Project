using System;
using UnityEngine;

public class Lava_Script : MonoBehaviour
{
    [SerializeField] private Transform objective;
    [SerializeField] private float lava_Speed;
    [SerializeField] private bool objective_Reached = false;


    private void Update()
    {
        if (objective_Reached) return;
        
        transform.position = Vector3.MoveTowards(transform.position, objective.position, lava_Speed * Time.deltaTime);

        if (transform.position.y >= objective.position.y)
        {
            objective_Reached = true;
        }
        
    }
}
