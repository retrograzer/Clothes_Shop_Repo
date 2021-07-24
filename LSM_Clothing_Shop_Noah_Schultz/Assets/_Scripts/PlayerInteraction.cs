using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used for all of the interactions the player has with the shopkeep and wardrobe.
/// Also switches on and off the panel with the helpful interact textbox.
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    public GameObject interactPanel, shopMenu, wardrobeMenu, dialoguePanel;

    public Text dialogueText;

    private bool shopInteract = false, shopOpen = false, wardrobeInteract = false, wardrobeOpen = false, dialogueAdvance = false;

    void Update()
    {
        if (shopInteract && Input.GetButtonDown("Interact"))
        {
            //If the shop is already open vs if it isn't open yet
            if (shopOpen == false)
            {
                //Debug.Log("Interacted with a thing!");
                InteractPanelSwitcher(false);
                ClothingMenuSwitcher(true);
            }
            else
            {
                InteractPanelSwitcher(true);
                ClothingMenuSwitcher(false);
            }
        }

        if (wardrobeInteract && Input.GetButtonDown("Interact"))
        {
            //If the Wardrobe is already open vs if it isn't open yet
            if (wardrobeOpen == false)
            {
                //Debug.Log("Interacted with a thing!");
                InteractPanelSwitcher(false);
                WardrobeMenuSwitcher(true);
            }
            else
            {
                InteractPanelSwitcher(true);
                WardrobeMenuSwitcher(false);
            }
        }

        //if in a dialogue area, you can either press the interact key or M1 to advance the dialogue to the menu or exit the dialogue with ESCAPE
        if (dialogueAdvance) 
        {
            if (Input.GetButtonDown("Interact") || Input.GetMouseButtonDown(0))
            {

            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Shopkeeper")
        {
            InteractPanelSwitcher(true);
            shopInteract = true;
        }
        if (collision.tag == "Wardrobe")
        {
            InteractPanelSwitcher(true);
            wardrobeInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Shopkeeper")
        {
            InteractPanelSwitcher(false);
            shopInteract = false;
        }
        if (collision.tag == "Wardrobe")
        {
            InteractPanelSwitcher(false);
            wardrobeInteract = false;
        }
    }

    //turns on/off the Interact Panel (the thing that says "E")
    void InteractPanelSwitcher(bool switchOn)
    {
        if (switchOn)
            interactPanel.SetActive(true);
        else
            interactPanel.SetActive(false);
    }

    //Turns on/off the Shop Menu
    public void ClothingMenuSwitcher(bool switchOn)
    {
        if (switchOn)
        {
            shopMenu.SetActive(true);
            shopOpen = true;
            GetComponent<PlayerMovement>().canMove = false;
        }
        else
        {
            shopMenu.SetActive(false);
            shopOpen = false;
            GetComponent<PlayerMovement>().canMove = true;
        }
    }

    //Turns on/off the Wardrobe Menu
    public void WardrobeMenuSwitcher(bool switchOn)
    {
        if (switchOn)
        {
            wardrobeMenu.SetActive(true);
            wardrobeOpen = true;
            wardrobeMenu.GetComponent<MenuNavigation>().RefreshGrids();
            GetComponent<PlayerMovement>().canMove = false;
        }
        else
        {
            wardrobeMenu.SetActive(false);
            wardrobeOpen = false;
            GetComponent<PlayerMovement>().canMove = true;
        }
    }

    public IEnumerator DialogueBoxStuff(string message, bool lastMessage)
    {
        dialogueAdvance = true;
        yield return new WaitForSeconds(0);
        if (lastMessage)
        {
            
        }
    }
}
