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
    private UIState currentState;

    private int currentScore;
    private int bestScore;


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

        ChangeState(UIState.Home);
    }

    private void Start()
    {
        currentScore = GameManager.Instance.CurrentScore;
        bestScore = GameManager.Instance.BestScore;
    }


    public void SetPlayGame()
    {
        ChangeState(UIState.Game);
    }

    public void SetGameOver()
    {
        ChangeState(UIState.GameOver);
        gameOverUI.SetResultGameOverScore(currentScore, bestScore);
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
