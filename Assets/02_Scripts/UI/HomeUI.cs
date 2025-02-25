using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeUI : BaseUI
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button OnoptionButton;
    [SerializeField] private Button offOptionButton;
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private GameObject righSelct;
    [SerializeField] private GameObject leftSelect;

    public AudioClip ButtonClip;

    

    public override void Init(UIManager uIManager)
    {
        base.Init(uIManager);

        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
        OnoptionButton.onClick.AddListener(OnClickOpenOptionPanel);
        offOptionButton.onClick.AddListener(OnClikOffOptionPanel);
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        rightButton.onClick.AddListener(OnClickRightButton);
        leftButton.onClick.AddListener(OnClickLeftButton);
    }

    public void OnClickStartButton()
    {
        GameManager.Instance.StartGame();
        AudioManager.PlayClip(ButtonClip);
    }

    public void OnClickExitButton()
    {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        AudioManager.PlayClip(ButtonClip);

    }

    public void OnClickOpenOptionPanel()
    {
        optionPanel.SetActive(true);
        float x_Jump = PlayerPrefs.GetFloat("JumpButtonX");
        if (x_Jump > MiddlePoint())
        {
            righSelct.SetActive(true);
            leftSelect.SetActive(false);
        }
        else
        {
            righSelct.SetActive(false);
            leftSelect.SetActive(true);
        }
        AudioManager.PlayClip(ButtonClip);
    }

    public void OnClikOffOptionPanel()
    {
        optionPanel.SetActive(false);
        AudioManager.PlayClip(ButtonClip);
    }

    public void SetBGMVolume(float value)
    {
        AudioManager.instance.MusicAudioSource.volume = Mathf.Clamp(value, 0f, 1f);
    }

    public void SetSFXVolume(float value)
    {
        AudioManager.instance.SetSoundEffectVolume(value);
    }

    public float MiddlePoint()
    {
        float x_spot;
        x_spot = (uiManager.gameUI.JumpButton.transform.position.x + uiManager.gameUI.SlideButton.transform.position.x) / 2.0f;

        return x_spot;
    }


    public void OnClickRightButton()
    {    
        if(uiManager.gameUI.JumpButton.transform.position.x < MiddlePoint())
        {      
            uiManager.ChangeButton();
            righSelct.SetActive(true);
            leftSelect.SetActive(false);
            AudioManager.PlayClip(ButtonClip);
        }
           
    }

    public void OnClickLeftButton()
    {
        if(uiManager.gameUI.JumpButton.transform.position.x > MiddlePoint())
        {
            uiManager.ChangeButton();
            righSelct.SetActive(false);
            leftSelect.SetActive(true);
            AudioManager.PlayClip(ButtonClip);
        }
           
    }

    protected override UIState GetUIstate()
    {
        return UIState.Home;
    }

}
