using UnityEngine;

public class Quit_Button : MonoBehaviour
{
    
    public void Quit()
    {
        #if UNITY_EDITOR
                
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                
                Application.Quit();
        #endif
    }
}
