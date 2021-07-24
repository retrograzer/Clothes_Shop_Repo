using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Used for all of the interactions the player has with the shopkeep and wardrobe.
/// Also switches on and off the panel with the helpful interact textbox.
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    public GameObject interactPanel, shopMenu, wardrobeMenu, dialoguePanel;

    public Text dialogueText;

    private bool shopInteract = false, shopOpen = false, wardrobeInteract = false, wardrobeOpen = false, dialogueAdvance = false;

    private string[] shopkeepDialogue_1 = new string[] { 
        "Welcome to Shirt Shack...I guess. (E)",
        "We have a great selection of...uh...really nice clothes. (E)",
        "Look, just buy something, will you? (E)",
    };
    private string[] shopkeepDialogue_2 = new string[] {
        "Welcome to Shirt Sha-...oh, it's you again. (E)",
        "Look, just buy something, will you? I'm almost on my break. (E)",
    };
    private string[] shopkeepDialogue_3 = new string[] {
        "What am I having to eat on my break? (E)",
        "What a weird question. (E)",
        "If you must know, it's a ham and cheese on rye, with a secret ingredient. (E)",
        "Of course I won't tell you the secret ingredient! Buy something and leave me alone. (E)"
    };
    private string[] shopkeepDialogue_4 = new string[] {
        "UGH...fine, if I tell you, will you leave me alone? (E)",
        "... (E)",
        "The secret ingredient is... (E)",
        "Pixie sticks. Don't look at me like that! It's good! (E)",
        "The sweet, tart, crunchiness of the sugary crystals perfectly compliments the umami of the ham and cheese! (E)",
        "Fine, call me weird all you want. I'm going to enjoy my sandwich. (E)"
    };
    private int shopkeepD_pointer = 0, shopkeepD_pointer2 = 0, shopkeepD_pointer3 = 0, shopkeepD_pointer4 = 0, shopDialogueRotation = 0;

    void Update()
    {
        if (shopInteract && Input.GetButtonDown("Interact"))
        {
            //If the shop is already open vs if it isn't open yet
            if (shopOpen == false)
            {
                //Debug.Log("Interacted with a thing!");
                InteractPanelSwitcher(false);
                //Debug.Log("D: " + shopkeepD_pointer + " " + shopkeepDialogue.Length);
                DialogueStuff();
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

        if (!wardrobeInteract && !shopInteract && !dialogueAdvance && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    //Just holds the logic for dialogue boxes for shopkeeper
    void DialogueStuff ()
    {
        Debug.Log("Rot: " + shopDialogueRotation);
        switch (shopDialogueRotation)
        {
            case 0:
                if (shopkeepD_pointer <= shopkeepDialogue_1.Length - 1)
                {
                    dialoguePanel.SetActive(true);
                    dialogueText.text = shopkeepDialogue_1[shopkeepD_pointer];
                    shopkeepD_pointer++;
                }
                else
                {
                    dialogueText.text = "";
                    dialoguePanel.SetActive(false);
                    shopkeepD_pointer = 0;
                    ClothingMenuSwitcher(true);
                    if (shopDialogueRotation < 3)
                    {
                        shopDialogueRotation++;
                    }
                }
                break;
            case 1:
                if (shopkeepD_pointer2 <= shopkeepDialogue_2.Length - 1)
                {
                    dialoguePanel.SetActive(true);
                    dialogueText.text = shopkeepDialogue_2[shopkeepD_pointer2];
                    shopkeepD_pointer2++;
                }
                else
                {
                    dialogueText.text = "";
                    dialoguePanel.SetActive(false);
                    shopkeepD_pointer2 = 0;
                    ClothingMenuSwitcher(true);
                    if (shopDialogueRotation < 3)
                    {
                        shopDialogueRotation++;
                    }
                }
                break;
            case 2:
                if (shopkeepD_pointer3 <= shopkeepDialogue_3.Length - 1)
                {
                    dialoguePanel.SetActive(true);
                    dialogueText.text = shopkeepDialogue_3[shopkeepD_pointer3];
                    shopkeepD_pointer3++;
                }
                else
                {
                    dialogueText.text = "";
                    dialoguePanel.SetActive(false);
                    shopkeepD_pointer3 = 0;
                    ClothingMenuSwitcher(true);
                    if (shopDialogueRotation < 3)
                    {
                        shopDialogueRotation++;
                    }
                }
                break;
            case 3:
                if (shopkeepD_pointer4 <= shopkeepDialogue_4.Length - 1)
                {
                    dialoguePanel.SetActive(true);
                    dialogueText.text = shopkeepDialogue_4[shopkeepD_pointer4];
                    shopkeepD_pointer4++;
                }
                else
                {
                    dialogueText.text = "";
                    dialoguePanel.SetActive(false);
                    shopkeepD_pointer4 = 0;
                    ClothingMenuSwitcher(true);
                    if (shopDialogueRotation < 4)
                    {
                        shopDialogueRotation++;
                    }
                }
                break;
            case 4:
                dialoguePanel.SetActive(false);
                ClothingMenuSwitcher(true);
                break;
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
            shopMenu.GetComponent<MenuNavigation>().RefreshGrids();
            shopMenu.GetComponent<MenuNavigation>().ClearPreview();
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
            wardrobeMenu.GetComponent<MenuNavigation>().ClearPreview();
            GetComponent<PlayerMovement>().canMove = false;
        }
        else
        {
            wardrobeMenu.SetActive(false);
            wardrobeOpen = false;
            GetComponent<PlayerMovement>().canMove = true;
        }
    }
}
