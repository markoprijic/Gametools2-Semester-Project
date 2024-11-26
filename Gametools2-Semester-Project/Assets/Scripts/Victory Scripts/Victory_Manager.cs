using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory_Manager : MonoBehaviour
{
    [SerializeField] private GameObject final_Door;

    public void Victory_Condition_Met()
    {
        final_Door.SetActive(false);
    }

    public void Next_Scene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
}
