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

    // Gifting references
    public PotionRequestManager potionManager;
    public InventoryController playerInventory;

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

                // Handle potion request or fallback to regular dialogue
                HandlePotionRequest();
            }
        }

        if (dialogueText.text == dialogue[index])
        {
            contButton.SetActive(true);
        }
    }

    void HandlePotionRequest()
    {
        if (potionManager != null && potionManager.currentRequest != null &&
            potionManager.currentRequest.npcName == npcName)
        {
            string requiredPotion = potionManager.currentRequest.potionName;

            // Check if the inventory has the required potion
            if (playerInventory.HasItem(requiredPotion))
            {
                // Remove the potion from inventory
                playerInventory.RemoveItem(requiredPotion);

                // Mark the request as fulfilled
                potionManager.SatisfyRequest();

                // NPC response after receiving the correct potion
                dialogueText.text = $"Thank you KINDLY yass {requiredPotion}!";
            }
            else
            {
                // NPC response if the potion is not in the inventory
                dialogueText.text = $"Bitch I need a {requiredPotion}. You dont have it. Anyway..";
            }
        }
        else
        {
            // Fallback to regular dialogue
            dialogueText.text = dialogue[index];
            StartCoroutine(Typing());
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
