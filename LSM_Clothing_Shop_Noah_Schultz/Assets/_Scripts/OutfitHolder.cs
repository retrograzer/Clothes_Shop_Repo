using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to switch out the Player's graphics.
/// </summary>
public class OutfitHolder : MonoBehaviour
{
    public SpriteRenderer head, hat, shirt, pants;

    public void ChangeHat (Sprite newhat)
    {
        hat.sprite = newhat;
    }

    public void ChangeShirt(Sprite newshirt)
    {
        shirt.sprite = newshirt;
    }

    public void ChangePants(Sprite newpants)
    {
        pants.sprite = newpants;
    }

    public void ChangeHead(Sprite newhead)
    {
        head.sprite = newhead;
    }
}
