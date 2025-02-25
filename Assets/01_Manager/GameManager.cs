using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private UIManager uiManager;

    public ResourceController resourceController = null;

    public StageManager stageManager = null;

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

    // ȹ���� ���� Ű
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
        resourceController = FindObjectOfType<ResourceController>();
        stageManager = FindObjectOfType<StageManager>();
        bestScore = PlayerPrefs.GetInt(BestScoreKey,0);
        coin = PlayerPrefs.GetInt(CoinKey, 0);
    }


    private void Start()
    {
        //StartCoroutine(IncreaseSpeedOverTime(2f));
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

    public void AddCoin(int _coin)
    {
        coin += _coin;
        uiManager.gameUI.AcquireCoin(coin);
    }

    // �����ð� ������ ��ֹ� �����̳� �ð� �����ϴ� �޼���
    private IEnumerator IncreaseObstacleSpeedOverTime(float interval)
    {
        while (true)  // ���� �ݺ� (������ ������ ����)
        {
            yield return new WaitForSeconds(interval);  // ���� �ð� ���
        }
    }


}
