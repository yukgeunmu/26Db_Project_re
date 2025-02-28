using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRelatedAchivements : MonoBehaviour, IAchivement
{
    public GameObject AchivementUI;

    public bool ZeroScore { get; private set; } = false;
    public bool SevenScore { get; private set; } = false;
    public bool HundreadScore { get; private set; } = false;

    public GameObject zeroScoreUI;
    public GameObject sevenScoreUI;
    public GameObject hundreadScoreUI;


    public void Start()
    {
        GameManager.Instance.OnScroeValueChanged += UpdateIsAchieved;   
    }

    public void UpdateIsAchieved()
    {
        if(GameManager.Instance.CurrentScore >= 0) ZeroScore = true;
        if(GameManager.Instance.CurrentScore >= 7) SevenScore = true;
        if(GameManager.Instance.CurrentScore >= 100) HundreadScore = true;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if(ZeroScore) zeroScoreUI.SetActive(true);
        if (SevenScore) sevenScoreUI.SetActive(true);
        if (HundreadScore) hundreadScoreUI.SetActive(true);
    }

    public void CloseAchivementUI()
    {
        AchivementUI.SetActive(false);
    }
}