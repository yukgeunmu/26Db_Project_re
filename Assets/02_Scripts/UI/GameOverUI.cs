using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : BaseUI
{

    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text bestScoreText;

    public override void Init(UIManager uIManager)
    {
        base.Init(uIManager);

        scoreText = transform.Find("Score").GetComponent<Text>();
        bestScoreText = transform.Find("BestScore").GetComponent<Text>();

        restartButton.onClick.AddListener(OnClickRestartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickRestartButton()
    {
        uiManager.SetPlayGame();
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }

    public void SetResultGameOverScore(int currentScore, int bestScore)
    {
        scoreText.text = currentScore.ToString();

        if(bestScore < currentScore)
        {
            bestScore = currentScore;
        }

        PlayerPrefs.SetInt("BestScore", bestScore);

        bestScoreText.text = bestScore.ToString();
    }

    protected override UIState GetUIstate()
    {
        return UIState.GameOver;
    }


}
