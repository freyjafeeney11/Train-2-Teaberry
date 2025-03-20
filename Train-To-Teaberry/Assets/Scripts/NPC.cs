using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    public GameObject contButton;
    private int index;
    public float wordSpeed;
    public bool playerIsClose;

    // Potion system references
    public PotionRequestManager potionManager;
    public InventoryController playerInventory;
    public GameObject giftUI; // UI for selecting a potion

    private string npcName;

    void Start()
    {
        npcName = gameObject.name; // Assign NPC's name dynamically
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                DisplayNPCDialogue();
                StartCoroutine(Typing());
            }
        }

        if (dialogueText.text == dialogue[index])
        {
            contButton.SetActive(true);
        }
    }

    void DisplayNPCDialogue()
    {
        if (potionManager != null && potionManager.currentRequest != null && potionManager.currentRequest.npcName == npcName)
        {
            dialogueText.text = $"Hello! I need a {potionManager.currentRequest.potionName}. Can you help?";
        }
        else
        {
            dialogueText.text = dialogue[index];
        }
    }

    IEnumerator Typing()
    {
        dialogueText.text = "";
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        contButton.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    public void OnGiftButton()
    {
        if (potionManager != null && potionManager.currentRequest != null && potionManager.currentRequest.npcName == npcName)
        {
            giftUI.SetActive(true); // Open inventory UI for selection
        }
    }

    public void GiftPotion(string potionName)
    {
        if (potionManager.DeliverPotion(potionName, playerInventory))
        {
            dialogueText.text = $"Thank you for the {potionName}!";
            giftUI.SetActive(false);
        }
        else
        {
            dialogueText.text = "This isn't what I needed...";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }
}
