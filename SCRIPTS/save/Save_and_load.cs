using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_and_load
{
    public static void saving(Saver dataSave)
    {
        Debug.Log("saving...");
        string key = "infirmation";
        string value = JsonUtility.ToJson(dataSave);
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    public static Saver loading()
    {
        
        Debug.Log("try_loading"); 
        string key = "infirmation";

        if (PlayerPrefs.HasKey(key))
        {
            Debug.Log("loading");
            string value = PlayerPrefs.GetString(key);
            Saver data = JsonUtility.FromJson<Saver>(value);
            return data;
        }
        return create_data();
    }

    public static Saver create_data()
    {
        Saver data = new Saver()
        {
            lastCompletedLevel = 0,
            haveLevelStar = new bool[100],
            offerRate = true
        };

        return data;
    }
}

public class Saver
{

    public int lastCompletedLevel;
    public bool[] haveLevelStar;
    public bool offerRate;
}

