using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public Image[] tabImages;
    public GameObject[] pages;
    void Start()
    {
        // Open on guide page
        ActivateTab(0);
    }

    public void ActivateTab(int tabNo)
    {
        Debug.Log("ActivateTab called with tabNum: " + tabNo);
        for(int i = 0; i < pages.Length; i++){
            pages[i].SetActive(false);
            tabImages[i].color = Color.grey;
        }
        pages[tabNo].SetActive(true);
        tabImages[tabNo].color = Color.white;
    }
}
