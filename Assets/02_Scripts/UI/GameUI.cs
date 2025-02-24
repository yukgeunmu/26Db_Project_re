using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameUI : BaseUI
{

    [SerializeField] private Slider hpSlider;
    [SerializeField] private Text coinText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private Button jumpButton;
    [SerializeField] private Button slideButton;

    public AudioClip jumpSound;
    public AudioClip slideSound;

    public override void Init(UIManager uIManager)
    {
        base.Init(uIManager);
        hpSlider = transform.Find("Slider").GetComponent<Slider>();
        coinText = transform.Find("CointText").GetComponent<Text>();
        scoreText = transform.Find("ScoreText").GetComponent<Text>();
        bestScoreText = transform.Find("BestScoreText").GetComponent<Text>();
        jumpButton.onClick.AddListener(OnClickJumpButton);
        slideButton.onClick.AddListener(OnClickSlideButton);
    }

    private void Start()
    {
        UpdateHPSlider(1);     
    }

    public void UpdateHPSlider(float percentage)
    {
        hpSlider.value = percentage;
    }

    // 점수 업데이트
    public void UpdateScore(int currentScore, int _bestScore)
    {
        scoreText.text = currentScore.ToString();
        bestScoreText.text = _bestScore.ToString();
    }


    public void OnClickJumpButton()
    {
        AudioManager.PlayClip(jumpSound);
    }

    public void OnClickSlideButton()
    {
        AudioManager.PlayClip(slideSound);
    }


    protected override UIState GetUIstate()
    {
        return UIState.Game;
    }

}
