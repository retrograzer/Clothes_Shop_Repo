using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script attached to the buttons on the shop items.
/// </summary>
public class GridItem : MonoBehaviour
{
    public string itemName = "";
    public int itemPrice = 0, itemID;
    public Sprite itemSprite;
    public string itemCategory = "";
    public Button me;
    public bool buying = true, owned = false;

    private void Start()
    {
        //owned = ClothingFileManagement.GetItemOwner(itemID);
        transform.GetChild(0).gameObject.GetComponent<Image>().sprite = itemSprite;
        me = gameObject.GetComponent<Button>();
        if (owned)
        {
            itemPrice = 0;
        }
        me.onClick.AddListener(TaskOnClick);
        //Debug.Log("Owned: " + owned + " " + itemName);
    }

    void TaskOnClick ()
    {
        GameObject.FindGameObjectWithTag("ClothingMenu").GetComponent<MenuNavigation>().SelectClothing(this);
    }
}
