using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Holds the Coin Counter and also the warning messages, just because I didn't want to make a whole new script
/// for those kinds of methods.
/// </summary>
public class PlayerCoinCount : MonoBehaviour
{
    [HideInInspector]
    public static int coinCount;

    public Text coinCountText, warningText, positiveText;
    public GameObject warningGO, positiveGO;
    public float warningTime;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            AddCoins(50);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            RemoveCoins(10);
        }
    }

    public void AddCoins (int added)
    {
        coinCount += added;
        if (coinCountText)
            coinCountText.text = "Coins: " + coinCount.ToString();
    }

    public void RemoveCoins (int removed)
    {
        coinCount -= removed;
        if (coinCountText)
            coinCountText.text = "Coins: " + coinCount.ToString();
    }

    /// <summary>
    /// A coroutine that can be called from anywhere to activate the warningmessage HUD element and display a warning message
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public IEnumerator WarningMessage(string message)
    {
        warningGO.SetActive(true);
        warningText.text = message.ToUpper();
        yield return new WaitForSeconds(warningTime);
        warningGO.SetActive(false);
        warningText.text = "WARNING";
    }

    public IEnumerator PositiveMessage(string message)
    {
        positiveGO.SetActive(true);
        positiveText.text = message.ToUpper();
        yield return new WaitForSeconds(warningTime);
        positiveGO.SetActive(false);
        positiveText.text = "WARNING";
    }
}
