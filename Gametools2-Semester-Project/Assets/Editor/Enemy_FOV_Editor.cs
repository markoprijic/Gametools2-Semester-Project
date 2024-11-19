using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Enemy_FOV))]
public class FieldOfViewEditor : Editor
{
    
    private void OnSceneGUI()
    {
        Enemy_FOV fov = (Enemy_FOV)target;
        
        // Draws circle in editor to show detection radius
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.FOV_Radius);
        
        
        // Left + Right sides of viewing cone
        Vector3 view_Angle_01 = Direction_From_Angle(fov.transform.eulerAngles.y, -fov.FOV_Angle / 2);
        Vector3 view_Angle_02 = Direction_From_Angle(fov.transform.eulerAngles.y, fov.FOV_Angle / 2);
        
        Handles.color = Color.yellow;
        // Stops angle indicator line from leaving radius circle
        Handles.DrawLine(fov.transform.position, fov.transform.position + view_Angle_01 * fov.FOV_Radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + view_Angle_02 * fov.FOV_Radius);
        
        
        // Player detection line
        if (fov.player_Visible)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.player_Ref.transform.position);
        }
    }// end OnSceneGUI

    private Vector3 Direction_From_Angle(float euler_Y, float angle_In_Degrees)
    {
        angle_In_Degrees += euler_Y;

        return new Vector3(Mathf.Sin(angle_In_Degrees * Mathf.Deg2Rad), 0, Mathf.Cos(angle_In_Degrees * Mathf.Deg2Rad));
    }// end Direction_From_Angle
    
}
