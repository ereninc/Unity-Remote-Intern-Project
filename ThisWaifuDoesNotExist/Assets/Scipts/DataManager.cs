using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public int playerScore = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
}
