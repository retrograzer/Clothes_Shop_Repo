using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used for navigating the menu that is used when shopping and accessing the wardrobe
/// This Script will be called by several of the menu buttons via the OnClick stuff
/// </summary>
public class MenuNavigation : MonoBehaviour
{

    public VerticalLayoutGroup tabvl;
    public GameObject menuHolder;
    public Text costText, itemNamePreviewText;
    public Image hatPreview, shirtPreview, pantPreview;
    public Sprite d_Hat, d_Shirt, d_Pant;
    public bool shopMenu = true;
    public List<FillItemGrid> itemGrids = new List<FillItemGrid>();
    
    List<GameObject> allMenus = new List<GameObject>();
    List<GameObject> allTabs = new List<GameObject>();
    GridItem currentlySelectedItem;
    PlayerCoinCount cc;
    OutfitHolder oh;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < tabvl.transform.childCount; i++) //fill allTabs with all of the tabs in the Vertical Layout group
        {
            if (tabvl.transform.GetChild(i).GetComponent<Button>())
            { //Make sure that all the tabs are the buttons, instead of anything else I accidentally put in there
                allTabs.Add(tabvl.transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < menuHolder.transform.childCount; i++) //fill allMenus with all of the menus in Holder
        {
            allMenus.Add(menuHolder.transform.GetChild(i).gameObject);
        }

        //Debug.Log(Stringify(allTabs) + " " + Stringify(allMenus));

        cc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCoinCount>();
        oh = GameObject.FindGameObjectWithTag("Player").GetComponent<OutfitHolder>();

        ChangeTab(0); //set it do default to shirts when opening

        ClearPreview();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {

        }
    }

    /// <summary>
    /// Tab 0 is shirts
    /// Tab 1 is pants
    /// Tab 2 is hats
    /// </summary>
    /// <param name="tabNum"></param>
    public void ChangeTab (int tabNum)
    {
        for (int i = 0; i < allTabs.Count; i++)
        {
            if (i != tabNum)
            {
                allMenus[i].SetActive(false);
            }
            else
            {
                allMenus[i].SetActive(true);
            }
        }
        ClearPreview();
    }

    /// <summary>
    /// Called when I press a button in the menu selecting a piece. 
    /// It takes the values of GridItem and puts them in the menu.
    /// </summary>
    /// <param name="item"></param>
    public void SelectClothing(GridItem item)
    {
        currentlySelectedItem = item;
        itemNamePreviewText.text = item.itemName;
        costText.text = "Cost: " + item.itemPrice;
        if (item.itemCategory.ToLower() == "shirt")
        {
            shirtPreview.sprite = item.itemSprite;
        }
        if (item.itemCategory.ToLower() == "pants")
        {
            pantPreview.sprite = item.itemSprite;
        }
        if (item.itemCategory.ToLower() == "hat")
        {
            hatPreview.sprite = item.itemSprite;
        }
    }

    /// <summary>
    /// Actually buying the damn thing. It takes the stored variable from the SelectedItem method
    /// And uses it's GridItem info to both change the player into the outfit and save the "owned" state
    /// </summary>
    public void BuySelection()
    {
        //Not enough coins & you don't have it yet
        if (currentlySelectedItem.itemPrice > PlayerCoinCount.coinCount && currentlySelectedItem.owned == false)
        {
            cc.StartCoroutine("WarningMessage", "NOT ENOUGH COINS");
            return;
        }
        //You already own it and are in the shop menu.
        else if (currentlySelectedItem.owned)
        {
            Debug.Log("Item Owned");
            cc.StartCoroutine("WarningMessage", "Item Already Owned");

            switch (currentlySelectedItem.itemCategory)
            {
                case ("shirt"):
                    oh.ChangeShirt(currentlySelectedItem.itemSprite);
                    d_Shirt = currentlySelectedItem.itemSprite;
                    break;
                case ("pants"):
                    oh.ChangePants(currentlySelectedItem.itemSprite);
                    d_Pant = currentlySelectedItem.itemSprite;
                    break;
                case ("hat"):
                    oh.ChangeHat(currentlySelectedItem.itemSprite);
                    d_Hat = currentlySelectedItem.itemSprite;
                    break;
            }
            return;
        }
        //buy the damn shirt
        else
        {
            Debug.Log("Bought: " + currentlySelectedItem.itemName);
            cc.StartCoroutine("PositiveMessage", "You bought " + currentlySelectedItem.itemName + "!");
            cc.RemoveCoins(currentlySelectedItem.itemPrice);
            currentlySelectedItem.owned = true;
            FileManagementOnBuy(currentlySelectedItem.itemID);
            switch (currentlySelectedItem.itemCategory)
            {
                case ("shirt"):
                    oh.ChangeShirt(currentlySelectedItem.itemSprite);
                    d_Shirt = currentlySelectedItem.itemSprite;
                    break;
                case ("pants"):
                    oh.ChangePants(currentlySelectedItem.itemSprite);
                    d_Pant = currentlySelectedItem.itemSprite;
                    break;
                case ("hat"):
                    oh.ChangeHat(currentlySelectedItem.itemSprite);
                    d_Hat = currentlySelectedItem.itemSprite;
                    break;
            }
            RefreshGrids();
        }
        //foreach (FillItemGrid index in itemGrids) //After buying the product, update the listings.
        //{
        //    index.RefreshItemGrid();
        //}
    }

    /// <summary>
    /// Used for the Wardrobe. Anything that is selected should already be bought if it's in the wardrobe menu, so with this it's just equipping.
    /// </summary>
    public void EquipSelection ()
    {
        cc.StartCoroutine("PositiveMessage", "You Equipped " + currentlySelectedItem.itemName + "!");
        switch (currentlySelectedItem.itemCategory)
        {
            case ("shirt"):
                oh.ChangeShirt(currentlySelectedItem.itemSprite);
                d_Shirt = currentlySelectedItem.itemSprite;
                break;
            case ("pants"):
                oh.ChangePants(currentlySelectedItem.itemSprite);
                d_Pant = currentlySelectedItem.itemSprite;
                break;
            case ("hat"):
                oh.ChangeHat(currentlySelectedItem.itemSprite);
                d_Hat = currentlySelectedItem.itemSprite;
                break;
        }
    }

    public void RefreshGrids ()
    {
        foreach (FillItemGrid index in itemGrids)
        {
            index.RefreshItemGrid();
        }
    }

    public void ClearPreview()
    {
        shirtPreview.sprite = d_Shirt;
        pantPreview.sprite = d_Pant;
        hatPreview.sprite = d_Hat;
    }

    //Just an override for ToString so I can see the names of gamebject lists for debugging
    public string Stringify(List<GameObject> obj)
    {
        string finalstring = "Objects in the List:";
        foreach (GameObject index in obj)
        {
            finalstring = finalstring + " " + index.name + ",";
        }
        return finalstring;
    }

    void FileManagementOnBuy (int ID)
    {
        switch (ID)
        {
            case 1:
                ClothingFileManagement.fit1 = true;
                break;
            case 2:
                ClothingFileManagement.fit2 = true;
                break;
            case 3:
                ClothingFileManagement.fit3 = true;
                break;
            case 4:
                ClothingFileManagement.fit4 = true;
                break;
            case 5:
                ClothingFileManagement.fit5 = true;
                break;
            case 11:
                ClothingFileManagement.fit11 = true;
                break;
            case 12:
                ClothingFileManagement.fit12 = true;
                break;
            case 13:
                ClothingFileManagement.fit13 = true;
                break;
            case 14:
                ClothingFileManagement.fit14 = true;
                break;
            case 15:
                ClothingFileManagement.fit15 = true;
                break;
            case 21:
                ClothingFileManagement.fit21 = true;
                break;
            case 22:
                ClothingFileManagement.fit22 = true;
                break;
            case 23:
                ClothingFileManagement.fit23 = true;
                break;
            case 24:
                ClothingFileManagement.fit24 = true;
                break;
            case 25:
                ClothingFileManagement.fit25 = true;
                break;
            default:
                Debug.LogError("You messed up in FIG, partner");
                break;
        }
    }
}
