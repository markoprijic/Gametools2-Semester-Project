using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory_Manager : MonoBehaviour
{
    [SerializeField] private GameObject final_Door;
    [SerializeField] private bool no_Door = false;
    
    public void Victory_Condition_Met()
    {
        if (no_Door)
            Next_Scene();
        final_Door.SetActive(false);
    }

    public void Next_Scene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
}
