using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Just a data storage device to put on the prefabs in Resources/_Prefabs
/// </summary>
public class ClothingItem : MonoBehaviour
{
    public string itemName, itemCategory;
    public int itemPrice, itemID;
    public Sprite itemSprite;
    //public bool owned;

    //private void Awake()
    //{
    //    owned = ClothingFileManagement.GetItemOwner(itemID);
    //}
}
