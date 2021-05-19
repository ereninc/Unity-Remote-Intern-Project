using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public int currentLevelScore = 0;
    public int totalGold;
    public Material playerMaterial;
    public Material playerWoodMaterial;

    private readonly SaveObject _saveObject = new SaveObject();
    private  SaveObject _loadedSavedObject = new SaveObject();
    private string json;
    
    private void Awake()
    {
        if (instance == null) instance = this;
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
            _loadedSavedObject = JsonUtility.FromJson<SaveObject>(saveString);
            totalGold = _loadedSavedObject.goldAmount;
        }
    }

    public void SaveGold()
    {
        totalGold += currentLevelScore;
        _saveObject.goldAmount += totalGold;
        json = JsonUtility.ToJson(_saveObject);
        currentLevelScore = 0;
        Debug.Log("Saved : "+json);
        File.WriteAllText(Application.dataPath + "/save.txt", json);
    }

    public int GetSavedTotalGold()
    {
        return totalGold;
    }

    public void EarnGold()
    {
        currentLevelScore += 10;
    }

    public void LoseGold()
    {
        currentLevelScore -= 10;
    }
}

public class SaveObject
{
    public int goldAmount;
}