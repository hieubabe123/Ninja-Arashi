using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveAndLoadManager : MonoBehaviour
{
    public static SaveAndLoadManager instance;

    //Save And Load From Script
    public DataManager DataManager { get; set; }



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void SaveGame()
    {
        SaveSystem.Save();
    }

    public static void LoadGame()
    {
        SaveSystem.Load();
    }


}
