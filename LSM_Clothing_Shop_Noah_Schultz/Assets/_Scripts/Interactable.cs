using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Put this on anything that I want the player to interact with.
/// This includes the shopkeep and the wardrobe, but I made an enum
/// so that I could modulate this as much as I want later.
/// </summary>
public class Interactable : MonoBehaviour
{
    public enum IType
    {
        shopkeeper,
        wardrobe
    }

    public IType interactableType;

    public void Activate ()
    {
        switch (interactableType)
        {
            case (IType.shopkeeper):
                Shopkeeper();
                break;
            case (IType.wardrobe):
                Wardrobe();
                break;
        }
    }

    void Shopkeeper ()
    {

    }

    void Wardrobe ()
    {

    }
}
