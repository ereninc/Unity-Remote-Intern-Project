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
    public int level;
    public Material playerMaterial;
    public Material playerWoodMaterial;
    [SerializeField] private GameObject player;

    private readonly SaveObject _saveObject = new SaveObject();
    private  SaveObject _loadedSavedObject = new SaveObject();
    private string json;
    
    private void Awake()
    {
        if (instance == null) instance = this;
        Load();
    }

    private void Start()
    {
        SetPlayer();
    }

    private void SetPlayer()
    {
        playerMaterial = player.GetComponent<Material>();
    }

    private void Load()
    {
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
        File.WriteAllText(Application.dataPath + "/save.txt", json);
    }

    public int GetSavedTotalGold()
    {
        return totalGold;
    }

    public void SetSavedTotalGold(int price)
    {
        if (totalGold >= price)
        {
            totalGold -= price;
        }
        //else cant buy
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
    public int level;
    public string playerColor;
    public string woodColor;
}