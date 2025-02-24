using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : BaseUI
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    public AudioClip ButtonClick;

    public override void Init(UIManager uIManager)
    {
        base.Init(uIManager);

        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);

    }

    public void OnClickStartButton()
    {
        GameManager.Instance.StartGame();
        AudioManager.PlayClip(ButtonClick);
    }

    public void OnClickExitButton()
    {
#if UNITY_EDITER
    UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        AudioManager.PlayClip(ButtonClick);

    }

    protected override UIState GetUIstate()
    {
        return UIState.Home;
    }

}
