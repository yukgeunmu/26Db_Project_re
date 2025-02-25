using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private UIManager uiManager;

    public AudioClip gameClip1;

    // 현재 점수
    private int currentscore = 0;
    public int CurrentScore => currentscore;

    // 최고 점수
    private int bestScore = 0;
    public int BestScore => bestScore;

    // 코인
    private int coin;
    public int Coin => coin;

    //최고 점수 키
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


    // 게임 시작 메서드
    public void StartGame()
    {
        uiManager.SetPlayGame();
        AudioManager.instance.ChangeBackGroundMusic(gameClip1);
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

    public void AddCoint(int _coin)
    {
        coin += _coin;
        uiManager.gameUI.AcquireCoin(coin);
    }

}
