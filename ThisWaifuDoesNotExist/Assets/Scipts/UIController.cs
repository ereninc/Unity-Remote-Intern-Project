using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject finishedMenu;
    [SerializeField] private TextMeshProUGUI levelScoreText;
    [SerializeField] private TextMeshProUGUI totalGoldText;
    [SerializeField] private TextMeshProUGUI levelText;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    void Update()
    {
        CheckWoodTake();
        StartImages();
        ShowFinishedMenu();
        SetLevelScoreText();
    }

    void SetLevelScoreText()
    {
        levelScoreText.text = (DataManager.instance.currentLevelScore).ToString();
        totalGoldText.text = DataManager.instance.totalGold.ToString();
    }

    void CheckWoodTake()
    {
        if (CollisionManager.instance.takeWood)
        {
            LeanTween.scale(scoreText, new Vector3(1, 1, 1), 0.1f);
        }
        else
        {
            LeanTween.scale(scoreText, new Vector3(0, 0, 1), 0.1f);
        }
    }

    void ShowFinishedMenu()
    {
        if (CollisionManager.instance.isFinished)
        {
            finishedMenu.SetActive(true);
        }
        else
        {
            finishedMenu.SetActive(false);
        }
    }

    void StartImages()
    {
        if (PlayerController.instance.isStarted)
        {
            startMenu.SetActive(false);
        }
        else
        {
            startMenu.SetActive(true);
        }
    }
    
    public void GetCollectedGold()
    {
        DataManager.instance.SaveGold();
        DataManager.instance.level += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SaveAfterShop()
    {
        DataManager.instance.SaveGold();
    }
}