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

    // 획득한 코인 키
    public const string CoinKey = "AcquireCoin";

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // 이벤트 구독
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
        coin = PlayerPrefs.GetInt(CoinKey, 0);

        // 초기 로드 시에도 참조 설정
        FindAndSetManagers();
    }


    public void Start()
    {
        if(resourceController != null && uiManager != null)
            StartCoroutine(TimeDamageLoop());
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

    public void AddCoin(int _coin)
    {
        coin += _coin;
        //uiManager.gameUI.AcquireCoin(coin);
    }

    // 시간에 따른 체력 감소
    private IEnumerator TimeDamageLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            resourceController.ChangeHealth(-timeDamage);
            uiManager.gameUI.UpdateHPSlider(resourceController.CurrentHealth/resourceController.MaxHealth);
        }
    }


    // 일정시간 지나면 장애물 생성이나 시간 변경하는 메서드
    private IEnumerator IncreaseObstacleSpeedOverTime(float interval)
    {
        while (true)  // 무한 반복 (게임이 끝나면 중지)
        {
            yield return new WaitForSeconds(interval);  // 일정 시간 대기
            // invokeRepeating
            // 장애물 생성 시간 변수
            // 장애물 갯수 변수
        }
    }

    public void ResetButtonPositions()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }


    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트 구독 해제
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
