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
    [SerializeField] private Button ChallengeButton;
    [SerializeField] private Button CustuMizingButton;
    [SerializeField] private Button StageButton;
    [SerializeField] private Button offOptionButton;
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private GameObject righSelct;
    [SerializeField] private GameObject leftSelect;
    [SerializeField] public GameObject stagePanel;




    public AudioClip ButtonClip;

    public override void Init(UIManager uIManager)
    {
        base.Init(uIManager);

        bgmSlider.value = AudioManager.instance.MusicVolume;
        sfxSlider.value = AudioManager.instance.SoundEffectVolume;

        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
        OnoptionButton.onClick.AddListener(OnClickOpenOptionPanel);
        offOptionButton.onClick.AddListener(OnClickOffOptionPanel);
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        rightButton.onClick.AddListener(OnClickRightButton);
        leftButton.onClick.AddListener(OnClickLeftButton);
        CustuMizingButton.onClick.AddListener(OnClickCustuMizingButton);
        ChallengeButton.onClick.AddListener(OnClickChallengeButton);
        StageButton.onClick.AddListener(OnClickStagePanel);

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

    public void OnClickOffOptionPanel()
    {
        optionPanel.SetActive(false);
        AudioManager.PlayClip(ButtonClip);
    }

    public void SetBGMVolume(float value)
    {
        AudioManager.instance.MusicAudioSource.volume = Mathf.Clamp(value, 0f, 1f);
        AudioManager.instance.SetMusicVolumeSave(value);
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
        if(uiManager.gameUI.jumpRect.anchoredPosition.x < 0)
        {      
            uiManager.ChangeButton();
            righSelct.SetActive(true);
            leftSelect.SetActive(false);
            AudioManager.PlayClip(ButtonClip);
        }
           
    }

    public void OnClickLeftButton()
    {
        if(uiManager.gameUI.jumpRect.anchoredPosition.x > 0)
        {
            uiManager.ChangeButton();
            righSelct.SetActive(false);
            leftSelect.SetActive(true);
            AudioManager.PlayClip(ButtonClip);
        }
           
    }

    public GameObject ChallengeUI;
    public void OnClickChallengeButton()
    {
        AudioManager.PlayClip(ButtonClip);
        ChallengeUI.SetActive(true);
    }

    public void OnClickCustuMizingButton()
    {
        SceneManager.LoadScene("CustomizingScene");
        GameManager.Instance.isTime = true;
        AudioManager.PlayClip(ButtonClip);
    }


    public void OnClickStagePanel()
    {
        stagePanel.SetActive(true);
    }


    protected override UIState GetUIstate()
    {
        return UIState.Home;
    }

}
