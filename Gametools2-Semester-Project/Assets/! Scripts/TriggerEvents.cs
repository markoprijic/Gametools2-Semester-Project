using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TriggerEvents : MonoBehaviour
{
    [Header("Trigger Settings")]
    [Tooltip("The tag of the object that can trigger this event (e.g., Player).")]
    public string triggeringTag = "Player";

    [Header("Event")]
    [Tooltip("Drag and drop the event to trigger when the player enters the collider.")]
    public UnityEvent onTriggerEnter;
    
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the specified tag
        if (other.CompareTag(triggeringTag))
        {
            // Invoke the UnityEvent
            onTriggerEnter.Invoke();
        }

    }

    public void LoadScene0()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadScene1()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadScene2()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadScene3()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadScene4()
    {
        SceneManager.LoadScene(4);
    }
}
