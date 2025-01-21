using UnityEngine;

public class ToggleObjectGroups : MonoBehaviour
{
    [Header("Object Groups")]
    [Tooltip("Drag and drop all GameObjects for Group 1 here.")]
    public GameObject[] group1;

    [Tooltip("Drag and drop all GameObjects for Group 2 here.")]
    public GameObject[] group2;

    private bool isGroup1Active = true;

    void Update()
    {
        // Check for input to toggle groups
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleGroups();
        }
    }

    void ToggleGroups()
    {
        // Toggle the active state
        isGroup1Active = !isGroup1Active;

        // Set the visibility of Group 1
        foreach (GameObject obj in group1)
        {
            if (obj != null)
                obj.SetActive(isGroup1Active);
        }

        // Set the visibility of Group 2
        foreach (GameObject obj in group2)
        {
            if (obj != null)
                obj.SetActive(!isGroup1Active);
        }
    }
}
