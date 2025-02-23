using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    Home,
    Game,
    GameOver
}


public class UIManager : MonoBehaviour
{

    public HomeUI homeUI;
    public GameUI gameUI;
    public GameOverUI gameOverUI;
    private UIState currentState;

    private int currentScore;
    private int bestScore;


    private void Awake()
    {
        homeUI = GetComponentInChildren<HomeUI>(true);
        homeUI.Init(this);
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI.Init(this);
        gameOverUI = GetComponentInChildren<GameOverUI>(true);
        gameOverUI.Init(this);

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
        gameUI.SetActive(currentState);
        gameOverUI.SetActive(currentState);
    }


}
