using System;
using UnityEngine;

public class Pause_Controller : MonoBehaviour
{
    [SerializeField] private GameObject pause_Menu;
    [SerializeField] private GameObject hp_Bar;
    [SerializeField] private GameObject crosshair;
    private bool is_Paused = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            is_Paused = !is_Paused;

        if (is_Paused == false) 
            Game_Unpaused();
        else if (is_Paused == true)
            Game_Paused();
        
    }

    private void Game_Paused()
    {
        pause_Menu.SetActive(true);
        hp_Bar.SetActive(false);
        crosshair.SetActive(false);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }// end Game_Paused()

    private void Game_Unpaused()
    {
        pause_Menu.SetActive(false);
        hp_Bar.SetActive(true);
        crosshair.SetActive(true);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }// end Game_Unpaused()
    
}// end script
