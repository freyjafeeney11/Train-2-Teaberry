using UnityEngine;

public class AlchemyWindow : MonoBehaviour
{
    public GameObject brewingPanel; // Assign the UI Panel in the Inspector
    public KeyCode toggleKey = KeyCode.B; // Set default key to 'B'

    void Start()
    {
        if (brewingPanel != null)
        {
            brewingPanel.SetActive(false); // Start hidden
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleBrewingPanel();
        }
    }

    void ToggleBrewingPanel()
    {
        if (brewingPanel != null)
        {
            brewingPanel.SetActive(!brewingPanel.activeSelf);
        }
    }
}
