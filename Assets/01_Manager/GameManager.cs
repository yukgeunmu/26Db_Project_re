using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private UIManager uiManager;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();

        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void StartGame()
    {
        uiManager.SetPlayGame();
    }

    public void GameOver()
    {
        uiManager.SetGameOver();
    }

}
