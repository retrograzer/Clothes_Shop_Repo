using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    
    /// <summary>
    /// 0 is demoscene, 1 is mute, 2 is unmute, 3 is quit
    /// </summary>
    /// <param name="index"></param>
    public void OnButtonClick (int index)
    {
        if (index == 0)
        {
            SceneManager.LoadScene(1);
        }
        else if (index == 1)
        {
            AudioListener.volume = 0;
        }
        else if (index == 2)
        {
            AudioListener.volume = 1;
        }
        else
        {
            Application.Quit();
        }
    }
}
