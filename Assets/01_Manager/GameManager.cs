using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UIManager uiManager;

    public ResourceController resourceController = null;

    public StageManager stageManager = null;

    public static bool isFirstSet = true;

    [SerializeField][Range(0f, 1f)] private float timeDamage = 1f;

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
            SceneManager.sceneLoaded += OnSceneLoaded; // �̺�Ʈ ����
        }
        else if(Instance != null)
        {
            Destroy(this.gameObject);
        }

        //ResetButtonPositions();  // ����

        //uiManager = FindObjectOfType<UIManager>();
        //resourceController = FindObjectOfType<ResourceController>();
        //stageManager = FindObjectOfType<StageManager>();

        bestScore = PlayerPrefs.GetInt(BestScoreKey,0);
        coin = PlayerPrefs.GetInt(CoinKey, 0);

        // �ʱ� �ε� �ÿ��� ���� ����
        FindAndSetManagers();
    }


    public void Start()
    {
        if(resourceController != null)
            StartCoroutine(TimeDamageLoop());


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

    private IEnumerator TimeDamageLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            resourceController.ChangeHealth(-timeDamage);
            uiManager.gameUI.UpdateHPSlider(resourceController.CurrentHealth/resourceController.MaxHealth);
        }
    }


    // �����ð� ������ ��ֹ� �����̳� �ð� �����ϴ� �޼���
    private IEnumerator IncreaseObstacleSpeedOverTime(float interval)
    {
        while (true)  // ���� �ݺ� (������ ������ ����)
        {
            yield return new WaitForSeconds(interval);  // ���� �ð� ���
        }
    }

    public void ResetButtonPositions()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }


    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // �̺�Ʈ ���� ����
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindAndSetManagers();
    }

    private void FindAndSetManagers()
    {
        uiManager = FindObjectOfType<UIManager>();
        resourceController = FindObjectOfType<ResourceController>();
        stageManager = FindObjectOfType<StageManager>();
    }


}
