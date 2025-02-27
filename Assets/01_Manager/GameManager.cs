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

    public bool ResetKey = false;

    // ���� ����
    private int currentscore = 0;
    public int CurrentScore => currentscore;

    // �ְ� ����
    private int bestScore = 0;
    public int BestScore => bestScore;
    [SerializeField][Range(0f, 10f)] private float maxTerminalVelocity = 10f;
    [SerializeField][Range(0f, 1f)] private float maxVelocity = 0.01f;

    [SerializeField][Range(0.1f, 10f)] private float obstacleSpeed = 10f;
    public float ObstacleSpeed => obstacleSpeed;

    [SerializeField][Range(0f, 10f)] private float obstacleTime = 1f;
    public float ObstacleTime => obstacleTime;

    [SerializeField][Range(0f, 1f)] private float obstacleFactor = 1f;
    public float ObstacleFactor => obstacleFactor;

    [SerializeField][Range(0f, 2f)] private float ItemspawnTime = 1f;
    public float ItemSpwnTime => ItemspawnTime;

    [SerializeField][Range(0f, 1f)] private float ItemspawnFactor = 0.05f;
    public float ItemSpawnFactor => ItemspawnFactor;
    [SerializeField][Range(0, 10)] private float itemGap;
    public float ItemGap => itemGap;


    public bool GodMode = false;

    // ����
    //private int coin;
    //public int Coin => coin;

    //�ְ� ���� Ű
    public const string BestScoreKey = "BestScore";

    // ȹ���� ���� Ű
    public const string CoinKey = "AcquireCoin";

    public bool isTime = false;

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

        if(ResetKey) ResetButtonPositions();

        //uiManager = FindObjectOfType<UIManager>();
        //resourceController = FindObjectOfType<ResourceController>();
        //stageManager = FindObjectOfType<StageManager>();

        bestScore = PlayerPrefs.GetInt(BestScoreKey,0);
        //coin = PlayerPrefs.GetInt(CoinKey, 0);

        // �ʱ� �ε� �ÿ��� ���� ����
        FindAndSetManagers();
    }


    public void Start()
    {

        if(resourceController != null && uiManager != null)
            StartCoroutine(TimeDamageLoop());
    }


    private void Update()
    {
        if (GodMode == true)
            resourceController.ChangeHealth(100);

      

        if (isTime)
        {
            Time.timeScale = 1f;
            obstacleSpeed = obstacleSpeed > maxTerminalVelocity ? maxTerminalVelocity : obstacleSpeed + maxVelocity;
        }
          
        else
            Time.timeScale = 0f;

        if (resourceController.CurrentHealth <= 0)
        {         
            GameOver();
            isTime = false;
        }
    }

    // ���� ���� �޼���
    public void StartGame()
    {
        uiManager.SetPlayGame();
        isTime = true;
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


    //// ���� ���ϴ� �޼���
    //public void AddScore(int score)
    //{
    //    currentscore += score;
    //    uiManager.gameUI.UpdateScore(currentscore, bestScore);
    //}

    public void AddScore(int _currentScore)
    {
        currentscore += _currentScore;
        uiManager.gameUI.UpdateScore(currentscore, bestScore);
    }

    // �ð��� ���� ü�� ����
    private IEnumerator TimeDamageLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            resourceController.ChangeHealth(-timeDamage);
            uiManager.gameUI.UpdateHPSlider(resourceController.CurrentHealth/resourceController.MaxHealth);
        }
    }

    public void ResetCurrentData()
    {
        currentscore = 0;
        obstacleSpeed = 2;
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
