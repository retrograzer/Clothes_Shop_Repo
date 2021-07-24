using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles all of the saving and loading of files.
/// Notation is provided below
/// </summary>
public class ClothingFileManagement : MonoBehaviour
{
    /*
     * File.Exists()
     * File.WriteAllLines
     */

    /*
     * Save File Structure
     * 
     *  public Redshirt = false,
		public Blueshirt = false,
		public Greenshirt = false,
		public Khakipants = false,
		public Denimpants = false,
		public Blackpants = false,
		public Hat1 = false,
		public Hat2 = false,
		public Hat3 = false,
     * 
     */

    public static string saveFilePath = @"SaveFile0.txt";

    public static string SAVE_FOLDER = @"/Assets/Saves/";

    public static bool fit1 = false;
    public static bool fit2 = false;
    public static bool fit3 = false;
    public static bool fit4 = false;
    public static bool fit5 = true;
    public static bool fit11 = false;
    public static bool fit12 = false;
    public static bool fit13 = false;
    public static bool fit14 = false;
    public static bool fit15 = true;
    public static bool fit21 = false;
    public static bool fit22 = false;
    public static bool fit23 = false;
    public static bool fit24 = false;
    public static bool fit25 = true;


    public void Awake()
    {
        SAVE_FOLDER = Application.dataPath + "/Saves/";
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public void Start()
    {

    }

    //This is the kinda stuff I was talking about in the Catalogue. this is such tedious coding, it's not elegant at all
    //But for right now, it works, and I'd rather have something ugly that works over something pretty that's buggy.
    //As a side note, 80s pop is a genre I never thought I'd revisit, but it's good for this kinda boring coding.
    public void ChangeFitBools (int ID)
    {
        switch (ID)
        {
            case (1):
                fit1 = true;
                break;
            case (2):
                fit2 = true;
                break;
            case (3):
                fit3 = true;
                break;
            case (4):
                fit4 = true;
                break;
            case (11):
                fit11 = true;
                break;
            case (12):
                fit12 = true;
                break;
            case (13):
                fit13 = true;
                break;
            case (14):
                fit14 = true;
                break;
            case (21):
                fit21 = true;
                break;
            case (22):
                fit22 = true;
                break;
            case (23):
                fit23 = true;
                break;
            case (24):
                fit24 = true;
                break;
            default:
                break;
        }
    }

    public static void Init()
    {
        Debug.Log("Initializing Saving_Loading...");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            //NewSaveFile(0);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            //JsonStuff();
        }
    }

    public void NewSaveFile(int fileID)
    {
        int temp = PlayerCoinCount.coinCount;

        SaveObject saveObject = new SaveObject
        {
            coinCount = temp,
            fit1 = false,
            fit2 = false,
            fit3 = false,
            fit4 = false,
            fit5 = false,
            fit11 = false,
            fit12 = false,
            fit13 = false,
            fit14 = false,
            fit15 = false,
            fit21 = false,
            fit22 = false,
            fit23 = false,
            fit24 = false,
            fit25 = false,
        };
        string json = JsonUtility.ToJson(saveObject, true);
        Debug.Log(json);

        File.WriteAllText(SAVE_FOLDER + saveFilePath[fileID], json);

    }

    public void LoadSaveFile(int fileID) //not at all finished
    {
        if (File.Exists(SAVE_FOLDER + saveFilePath[fileID]))
        {
            string lines = File.ReadAllText(SAVE_FOLDER + saveFilePath[fileID]);

            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(lines);
        }
    }

    //this gets called every time you want to save the game, hence the static. It takes PlayerPrefs to get current file, which is 
    //assigned in the New or Load File logic
    public static void OverriteExistingFile()
    {
        int fileID = PlayerPrefs.GetInt("FileID", 4);

        if (fileID >= 4)
        {
            Debug.LogError("Tried to Get Current file but failed. Aborting Override");
            return;
        }

        SaveObject saveObject = new SaveObject
        {
        };

        string json = JsonUtility.ToJson(saveObject, true);
        Debug.Log(json);

        File.WriteAllText(SAVE_FOLDER + saveFilePath, json);

        Debug.Log("Success Saving to " + saveFilePath);
    }

    //used to load from SaveObject whether an item is already bought or not-
    public static bool GetItemOwner (int ID) 
    {
        if (File.Exists(SAVE_FOLDER + saveFilePath))
        {
            string lines = File.ReadAllText(SAVE_FOLDER + saveFilePath);

            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(lines);

            if (lines[ID].Equals(true))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (ID == 5 || ID == 15 || ID == 25) //These are all the default options.
        {
            return true;
        }
        else //if there is no file,  just default to not having it.
        {
            return false;
        }
    }

    /// <summary>
    /// If you need an idea about which of these is which, look at ItemID_Catalogue in the Scripts folder
    /// </summary>
    private class SaveObject
    {
        public int coinCount;
        public bool fit1 = false;
        public bool fit2 = false;
        public bool fit3 = false;
        public bool fit4 = false;
        public bool fit5 = true;
        public bool fit11 = false;
        public bool fit12 = false;
        public bool fit13 = false;
        public bool fit14 = false;
        public bool fit15 = true;
        public bool fit21 = false;
        public bool fit22 = false;
        public bool fit23 = false;
        public bool fit24 = false;
        public bool fit25 = true;
    }
}
