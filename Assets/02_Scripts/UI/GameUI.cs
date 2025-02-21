using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameUI : BaseUI
{

    [SerializeField] private Slider hpSlider;
    [SerializeField] private Text coin;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text bestScoreText;

    public override void Init(UIManager uIManager)
    {
        base.Init(uIManager);
        hpSlider = transform.Find("Slider").GetComponent<Slider>();
        coin = transform.Find("Coin").GetComponent<Text>();
        scoreText = transform.Find("Score").GetComponent<Text>();
        bestScoreText = transform.Find("BestScore").GetComponent<Text>();
    }

    private void Start()
    {
        UpdateHPSlider(1);     
    }

    public void UpdateHPSlider(float percentage)
    {
        hpSlider.value = percentage;
    }

    public void SetScore(int currentScore, int _bestScore)
    {
        scoreText.text = currentScore.ToString();
        bestScoreText.text = _bestScore.ToString();
    }

    public void UpdateScore(int currentScore)
    {
        scoreText.text = currentScore.ToString();
    }


    protected override UIState GetUIstate()
    {
        return UIState.Game;
    }

}
