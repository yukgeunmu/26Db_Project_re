using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private UIManager uiManager;

    public AudioClip gameClip1;

    // ���� ����
    private int currentscore = 0;
    public int CurrentScore => currentscore;

    // �ְ� ����
    private int bestScore = 0;
    public int BestScore => bestScore;

    // ����
    private int coin;
    public int Coin => coin;

    //�ְ� ���� Ű
    public const string BestScoreKey = "BestScore";

    public const string CoinKey = "AcquireCoin";

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(Instance != null)
        {
            Destroy(this.gameObject);
        }

            uiManager = FindObjectOfType<UIManager>();
        bestScore = PlayerPrefs.GetInt(BestScoreKey,0);
        coin = PlayerPrefs.GetInt(CoinKey, 0);
    }


    // ���� ���� �޼���
    public void StartGame()
    {
        uiManager.SetPlayGame();
        AudioManager.instance.ChangeBackGroundMusic(gameClip1);
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

    public void AddCoint(int _coin)
    {
        coin += _coin;
        uiManager.gameUI.AcquireCoin(coin);
    }

}
