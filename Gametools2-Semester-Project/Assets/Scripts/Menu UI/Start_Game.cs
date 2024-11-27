using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Game : MonoBehaviour
{
    public void Start_Pressed()
    {
        SceneManager.LoadScene(1);
    }
}
