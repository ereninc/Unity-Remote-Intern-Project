using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject startText;
    [SerializeField] private GameObject arrowImage;
    [SerializeField] private GameObject fingerImage;
    [SerializeField] private GameObject finishedMenu;
    [SerializeField] private TextMeshProUGUI levelScoreText;

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
        levelScoreText.text = (ScoreManager.instance.playerScore * 10).ToString();
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
            startText.SetActive(false);
            arrowImage.SetActive(false);
            fingerImage.SetActive(false);
        }
        else
        {
            startText.SetActive(true);
            arrowImage.SetActive(true);
            fingerImage.SetActive(true);
        }
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(0);
    }
}