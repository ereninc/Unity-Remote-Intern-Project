using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public int totalGold;

    public void SaveGold()
    {
        PlayerPrefs.SetInt("Gold", totalGold);
    }

}
