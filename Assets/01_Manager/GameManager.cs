using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private UIManager uiManager;

    // ���� ����
    private int currentscore = 0;
    public int CurrentScore => currentscore;

    // �ְ� ����
    private int bestScore = 0;
    public int BestScore => bestScore;

    //�ְ� ���� Ű
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

    // ���ӿ��� �޼���
    public void GameOver()
    {
        if(currentscore > bestScore)
        {
            bestScore = currentscore;
        }
        PlayerPrefs.SetInt(BestScoreKey,bestScore);
        uiManager.SetGameOver();
    }


    // ���� ���ϴ� �޼���
    public void AddScore(int score)
    {
        currentscore += score;
        uiManager.gameUI.UpdateScore(currentscore, bestScore);
    }

}
