using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class GameUI : BaseUI
{

    [SerializeField] private Slider hpSlider;
    [SerializeField] private Text coinText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private Button jumpButton;
    public Button JumpButton => jumpButton;
    [SerializeField] private Button slideButton;
    public Button SlideButton => slideButton;

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

    // 코인 업데이트
    public void AcquireCoin(int coin)
    {
        coinText.text = coin.ToString();
    }


    public void OnClickJumpButton()
    {
        AudioManager.PlayClip(jumpSound);
    }

    public void OnClickSlideButton()
    {
        AudioManager.PlayClip(slideSound);
    }

    public void ChangeJumpButton()
    {
        Vector3 temptPosition = jumpButton.transform.position;
        jumpButton.transform.position = slideButton.transform.position;
        slideButton.transform.position = temptPosition;

        PlayerPrefs.SetFloat("JumpButtonX", jumpButton.transform.position.x);
        PlayerPrefs.SetFloat("JumpButtonY", jumpButton.transform.position.y);
        PlayerPrefs.SetFloat("JumpButtonZ", jumpButton.transform.position.z);

        PlayerPrefs.SetFloat("SlideButtonX", slideButton.transform.position.x);
        PlayerPrefs.SetFloat("SlideButtonY", slideButton.transform.position.y);
        PlayerPrefs.SetFloat("SlideButtonZ", slideButton.transform.position.z);

        PlayerPrefs.Save();
    }

    public void LoadButtonPositions()
    {
        // 저장된 값이 있는지 확인 후 불러오기
        if (PlayerPrefs.HasKey("JumpButtonX"))
        {
            jumpButton.transform.position = new Vector3(
                PlayerPrefs.GetFloat("JumpButtonX"),
                PlayerPrefs.GetFloat("JumpButtonY"),
                PlayerPrefs.GetFloat("JumpButtonZ")
            );

            slideButton.transform.position = new Vector3(
                PlayerPrefs.GetFloat("SlideButtonX"),
                PlayerPrefs.GetFloat("SlideButtonY"),
                PlayerPrefs.GetFloat("SlideButtonZ")
            );
        }
    }


    protected override UIState GetUIstate()
    {
        return UIState.Game;
    }

}
