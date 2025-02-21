using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : BaseUI
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    public override void Init(UIManager uIManager)
    {
        base.Init(uIManager);

        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);

    }

    public void OnClickStartButton()
    {
        uiManager.SetPlayGame();
    }

    public void OnClickExitButton()
    {
#if UNITY_EDITER
    UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }

    protected override UIState GetUIstate()
    {
        return UIState.Home;
    }

}
