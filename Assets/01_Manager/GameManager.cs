using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private UIManager uiManager;

    // 현재 점수
    private int currentscore = 0;
    public int CurrentScore => currentscore;

    // 최고 점수
    private int bestScore = 0;
    public int BestScore => bestScore;

    //최고 점수 키
    public const string BestScoreKey = "BestScore";

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();

        if(Instance == null)
        {
            Instance = this;
        }
        
        bestScore = PlayerPrefs.GetInt(BestScoreKey,0);
    }

    public void StartGame()
    {
        uiManager.SetPlayGame();
    }

    // 게임오버 메서드
    public void GameOver()
    {
        if(currentscore > bestScore)
        {
            bestScore = currentscore;
        }
        PlayerPrefs.SetInt(BestScoreKey,bestScore);
        uiManager.SetGameOver();
    }


    // 점수 더하는 메서드
    public void AddScore(int score)
    {
        currentscore += score;
        uiManager.gameUI.UpdateScore(currentscore, bestScore);
    }

}
