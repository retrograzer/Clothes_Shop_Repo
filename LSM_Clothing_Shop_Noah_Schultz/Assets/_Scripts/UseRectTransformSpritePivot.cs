using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Not made by Noah Schultz. Found this script online to use the pivot point of the sprites
/// to change the pivot point in the Rect Transform for the PreviewWindow
/// </summary>
public class UseRectTransformSpritePivot : MonoBehaviour
{
    //Not used
    void FixedUpdate()
    {
        Vector2 size = GetComponent<RectTransform>().sizeDelta;
        Vector2 pixelPivot = GetComponent<Image>().sprite.pivot;
        Vector2 percentPivot = new Vector2(pixelPivot.x / size.x, pixelPivot.y / size.y);
        GetComponent<RectTransform>().pivot = percentPivot;
    }

}
