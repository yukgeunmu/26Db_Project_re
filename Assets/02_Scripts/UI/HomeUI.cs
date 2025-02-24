using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : BaseUI
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button OnoptionButton;
    [SerializeField] private Button offOptionButton;
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    public AudioClip ButtonClick;

    public override void Init(UIManager uIManager)
    {
        base.Init(uIManager);

        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
        OnoptionButton.onClick.AddListener(OnClickOpenOptionPanel);
        offOptionButton.onClick.AddListener(OnClikOffOptionPanel);
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
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

    public void OnClickOpenOptionPanel()
    {
        optionPanel.SetActive(true);
    }

    public void OnClikOffOptionPanel()
    {
        optionPanel.SetActive(false);
    }

    public void SetBGMVolume(float value)
    {
        AudioManager.instance.MusicAudioSource.volume = Mathf.Clamp(value, 0f, 1f);
    }

    public void SetSFXVolume(float value)
    {
        AudioManager.instance.SetSoundEffectVolume(value);
    }

    protected override UIState GetUIstate()
    {
        return UIState.Home;
    }

}
