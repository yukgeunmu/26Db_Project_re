using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    Home,
    Game,
    GameOver,
    Stage
}


public class UIManager : MonoBehaviour
{

    public HomeUI homeUI;
    public GameUI gameUI;
    public GameOverUI gameOverUI;
    public StageUI stageUI;
    public StageManager stageManager;
    private UIState currentState;


    private void Awake()
    {
        homeUI = GetComponentInChildren<HomeUI>(true);
        homeUI.Init(this);
        if (TryGetComponentInChildren<GameUI>(out gameUI))
            gameUI.Init(this);

        if (TryGetComponentInChildren<GameOverUI>(out gameOverUI))
            gameOverUI.Init(this);

        stageUI = GetComponentInChildren<StageUI>(true);
        stageUI.Init(this);
        stageManager = FindObjectOfType<StageManager>();

    }

    private void Start()
    {
        if (GameManager.isFirstSet)
        {
            ChangeState(UIState.Home);
            gameUI.LoadButtonPositions();
            GameManager.isFirstSet = false;
        }
        else
        {
            GameManager.Instance.StartGame();
            gameUI.LoadButtonPositions();
        }

    }


    public void SetPlayGame()
    {
        ChangeState(UIState.Game);
    }

    public void SetHome()
    {
        ChangeState(UIState.Home);
    }

    public void SetGameOver()
    {
        ChangeState(UIState.GameOver);
        gameOverUI.SetResultGameOverScore(GameManager.Instance.CurrentScore, GameManager.Instance.BestScore);
    }

    public void ChangePlayerHP(float currentHP, float maxHP)
    {
        gameUI.UpdateHPSlider(currentHP/maxHP);
    }

    public void ChangeState(UIState state)
    {
        currentState = state;
        homeUI.SetActive(currentState);

        if(gameUI != null)
            gameUI.SetActive(currentState);
        if(gameOverUI != null)
            gameOverUI.SetActive(currentState);

        stageUI.SetActive(currentState);
  
    }

    public void ChangeButton()
    {
        gameUI.ChangeJumpButton();       
    }

    private bool TryGetComponentInChildren<T>(out T component) where T : MonoBehaviour
    {
        component = GetComponentInChildren<T>(true);
        return component != null;
    }


}
