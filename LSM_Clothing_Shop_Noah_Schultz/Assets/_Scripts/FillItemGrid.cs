using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Takes the prefabs in the Resources/_Prefabs folder and auto assigns them into the grid.
/// </summary>
// This code was brought to you by the Doom Eternal OST.
public class FillItemGrid : MonoBehaviour
{

    public enum IGType
    {
        shirt,
        pant,
        hat
    }

    public IGType gridType;
    public Sprite notOwnedSprite, ownedSprite;
    public bool wardrobe = false;

    [SerializeField]
    GameObject[] shirtPrefs, pantPrefs, hatPrefs;

    GameObject shirtButton, pantButton, hatButton;

    [SerializeField]
    List<GameObject> allSpawnedButtons = new List<GameObject>(); //I need this later if I'm going to refresh the lists


    private void Start()
    {
        shirtPrefs = Resources.LoadAll<GameObject>("_Prefabs/ClothingPrefabs/Shirts");
        pantPrefs = Resources.LoadAll<GameObject>("_Prefabs/ClothingPrefabs/Pants");
        hatPrefs = Resources.LoadAll<GameObject>("_Prefabs/ClothingPrefabs/Hats");

        shirtButton = Resources.Load<GameObject>("_Prefabs/MenuButtonPrefabs/ShirtButton");
        pantButton = Resources.Load<GameObject>("_Prefabs/MenuButtonPrefabs/PantsButton");
        hatButton = Resources.Load<GameObject>("_Prefabs/MenuButtonPrefabs/HatButton");

        Debug.Log("Shirts: " + Stringify(shirtPrefs) + " | Pants: " + Stringify(pantPrefs) + " | " + Stringify(hatPrefs));

        FillGrid();
    }

    //Fills each item grid based on their type.
    void FillGrid()
    {
        switch (gridType)
        {
            case (IGType.shirt):
                //Loops through all the shirts put in the shirtPrefs folder and assigns them to buttons.
                foreach (GameObject index in shirtPrefs)
                {
                    GameObject newShopButton = Instantiate(shirtButton);
                    newShopButton.transform.SetParent(transform);
                    allSpawnedButtons.Add(newShopButton);

                    GridItem x = newShopButton.GetComponent<GridItem>();
                    ClothingItem y = index.GetComponent<ClothingItem>();
                    //set the GridItem stats to what is in the Prefabs folder.
                    x.itemPrice = y.itemPrice;
                    x.itemSprite = y.itemSprite;
                    x.itemName = y.itemName;
                    x.itemCategory = y.itemCategory;
                    x.itemID = y.itemID;
                    x.owned = SetOwnership(x.itemID);
                    //Debug.Log("Owned: " + x.owned + " " + x.itemName);
                    //if you own it, change the button to indicate so.
                    if (x.owned)
                    {
                        newShopButton.GetComponent<Button>().image.sprite = ownedSprite;
                    }
                    else
                    {
                        //If we are working with a wardrobe, we don't want to show items we don't own.
                        if (!wardrobe)
                        {
                            newShopButton.GetComponent<Button>().image.sprite = notOwnedSprite;
                        }
                        else
                        {
                            allSpawnedButtons.Remove(newShopButton);
                            Destroy(newShopButton);
                        }
                    }
                }
                break;
            case (IGType.pant):
                foreach (GameObject index in pantPrefs)
                {
                    GameObject newShopButton = Instantiate(pantButton);
                    newShopButton.transform.SetParent(transform);
                    allSpawnedButtons.Add(newShopButton);

                    GridItem x = newShopButton.GetComponent<GridItem>();
                    ClothingItem y = index.GetComponent<ClothingItem>();
                    x.itemPrice = y.itemPrice;
                    x.itemSprite = y.itemSprite;
                    x.itemName = y.itemName;
                    x.itemCategory = y.itemCategory;
                    x.itemID = y.itemID;
                    x.owned = SetOwnership(x.itemID);
                    if (x.owned)
                    {
                        newShopButton.GetComponent<Button>().image.sprite = ownedSprite;
                    }
                    else
                    {
                        //If we are working with a wardrobe, we don't want to show items we don't own.
                        if (!wardrobe)
                        {
                            newShopButton.GetComponent<Button>().image.sprite = notOwnedSprite;
                        }
                        else
                        {
                            allSpawnedButtons.Remove(newShopButton);
                            Destroy(newShopButton);
                        }
                    }
                }
                break;
            case (IGType.hat):
                foreach (GameObject index in hatPrefs)
                {
                    GameObject newShopButton = Instantiate(hatButton);
                    newShopButton.transform.SetParent(transform);
                    allSpawnedButtons.Add(newShopButton);

                    GridItem x = newShopButton.GetComponent<GridItem>();
                    ClothingItem y = index.GetComponent<ClothingItem>();
                    x.itemPrice = y.itemPrice;
                    x.itemSprite = y.itemSprite;
                    x.itemName = y.itemName;
                    x.itemCategory = y.itemCategory;
                    x.itemID = y.itemID;
                    x.owned = SetOwnership(x.itemID);
                    if (x.owned)
                    {
                        newShopButton.GetComponent<Button>().image.sprite = ownedSprite;
                    }
                    else
                    {
                        //If we are working with a wardrobe, we don't want to show items we don't own.
                        if (!wardrobe)
                        {
                            newShopButton.GetComponent<Button>().image.sprite = notOwnedSprite;
                        }
                        else
                        {
                            allSpawnedButtons.Remove(newShopButton);
                            Destroy(newShopButton);
                        }
                    }
                }
                break;
        }
        
    }

    public void RefreshItemGrid ()
    {
        //Debug.Log("Refresh: ");
        foreach (GameObject index in allSpawnedButtons)
        {
            Destroy(index);
        }
        allSpawnedButtons.Clear();
        FillGrid();
    }

    //Gets if the item is already owned from CFM.
    public bool SetOwnership (int ID)
    {
        switch (ID)
        {
            case 1:
                return ClothingFileManagement.fit1;
            case 2:
                return ClothingFileManagement.fit2;
            case 3:
                return ClothingFileManagement.fit3;
            case 4:
                return ClothingFileManagement.fit4;
            case 5:
                return ClothingFileManagement.fit5;
            case 11:
                return ClothingFileManagement.fit11;
            case 12:
                return ClothingFileManagement.fit12;
            case 13:
                return ClothingFileManagement.fit13;
            case 14:
                return ClothingFileManagement.fit14;
            case 15:
                return ClothingFileManagement.fit15;
            case 21:
                return ClothingFileManagement.fit21;
            case 22:
                return ClothingFileManagement.fit22;
            case 23:
                return ClothingFileManagement.fit23;
            case 24:
                return ClothingFileManagement.fit24;
            case 25:
                return ClothingFileManagement.fit25;
            default:
                Debug.LogError("You messed up in FIG, partner");
                return false;
        }
    }

    public string Stringify(GameObject[] obj)
    {
        string finalstring = "Objects in the List:";
        foreach (GameObject index in obj)
        {
            finalstring = finalstring + " " + index.name + ",";
        }
        return finalstring;
    }
}
