using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public Image[] tabImages; // Array for the tab buttons
    public GameObject[] pages; // Array for the page content (e.g., Inventory, Guide, Settings, Potion Requests)

    public Text requestsTabText; // Reference to the Requests tab's Text component (for dynamic name change)

    private int activeRequestsCount = 0; // Example of active request count

    void Start()
    {

        // Open on guide page (or another default page, adjust as needed)
        ActivateTab(0);  // Assuming 0 is the index for Guide
    }

    // Method to activate a tab and display its corresponding page
    public void ActivateTab(int tabNo)
    {
        Debug.Log("ActivateTab called with tabNo: " + tabNo);

        // Loop through all the pages and hide them
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);    // Hide all pages
            tabImages[i].color = Color.grey;  // Set tab buttons to grey
        }

        // Activate the selected page
        pages[tabNo].SetActive(true);
        tabImages[tabNo].color = Color.white;  // Highlight the selected tab
    }

    // Call this method to update the text when a new request is received
    public void UpdateRequestsTabText(int requestCount)
    {
        if (requestsTabText != null)
        {
            activeRequestsCount = requestCount; // Update the active request count
            requestsTabText.text = "Potion Requests (" + activeRequestsCount + ")"; // Update the Requests tab text
        }
    }
}
