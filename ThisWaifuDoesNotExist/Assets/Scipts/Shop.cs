using System;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    public string woodColor = "";
    public string playerColor = "";
    [SerializeField] private TextMeshProUGUI goldText;
    private DataManager _dm;
    private int price = 500;
    [SerializeField] private Material greenEye;
    [SerializeField] private Material blueEye;
    [SerializeField] private Material redEye;
    [SerializeField] private Material pinkEye;

    private void Awake()
    {
        if (instance == null) instance = this;
        _dm = DataManager.instance;
    }

    private void Update()
    {
        goldText.text = _dm.GetSavedTotalGold().ToString();
    }

    public void SetColorRed()
    {
        if (_dm.GetSavedTotalGold() >= price)
        {
            woodColor = "Red";
            _dm.SetSavedTotalGold(price);
        }
    }
    public void SetColorBlue()
    {
        if (_dm.GetSavedTotalGold() >= price)
        {
            woodColor = "Blue";
            _dm.SetSavedTotalGold(price);
        }
    }
    public void SetColorGreen()
    {
        if (_dm.GetSavedTotalGold() >= price)
        {
            woodColor = "Green";
            _dm.SetSavedTotalGold(price);
        }
    }
    public void SetColorGrey()
    {
        if (_dm.GetSavedTotalGold() >= price)
        {
            woodColor = "Grey";
            _dm.SetSavedTotalGold(price);
        }
    }
    public void SetColorWhite()
    {
        if (_dm.GetSavedTotalGold() >= price)
        {
            woodColor = "White";
            _dm.SetSavedTotalGold(price);
        }
    }
    public void SetColorOrange()
    {
        if (_dm.GetSavedTotalGold() >= price)
        {
            woodColor = "Orange";
            _dm.SetSavedTotalGold(price);
        }
    }
    public void SetColorYellow()
    {
        if (_dm.GetSavedTotalGold() >= price)
        {
            woodColor = "Yellow";
            _dm.SetSavedTotalGold(price);
        }
    }
    public void SetColorPink()
    {
        if (_dm.GetSavedTotalGold() >= price)
        {
            woodColor = "Pink";
            _dm.SetSavedTotalGold(price);
        }
    }

    public void SetPlayerColorRed()
    {
        if (_dm.GetSavedTotalGold() >= price)
        {
            playerColor = "Red";
            _dm.SetSavedTotalGold(price);
        }
    }
    public void SetPlayerColorBlue()
    {
        if (_dm.GetSavedTotalGold() >= price)
        {
            playerColor = "Blue";
            _dm.SetSavedTotalGold(price);
        }
    }
    public void SetPlayerColorGreen()
    {
        if (_dm.GetSavedTotalGold() >= price)
        {
            playerColor = "Green";
            _dm.SetSavedTotalGold(price);
        }
    }
    public void SetPlayerColorGrey()
    {
        if (_dm.GetSavedTotalGold() >= price)
        {
            playerColor = "Grey";
            _dm.SetSavedTotalGold(price);
        }
    }
    public void SetPlayerColorWhite()
    {
        if (_dm.GetSavedTotalGold() >= price)
        {
            playerColor = "White";
            _dm.SetSavedTotalGold(price);
        }
    }
    public void SetPlayerColorOrange()
    {
        if (_dm.GetSavedTotalGold() >= price)
        {
            playerColor = "Orange";
            _dm.SetSavedTotalGold(price);
        }
    }
    public void SetPlayerColorYellow()
    {
        if (_dm.GetSavedTotalGold() >= price)
        {
            playerColor = "Yellow";
            _dm.SetSavedTotalGold(price);
        }
    }
    public void SetPlayerColorPink()
    {
        if (_dm.GetSavedTotalGold() >= price)
        {
            playerColor = "Pink";
            _dm.SetSavedTotalGold(price);
        }
    }
}
