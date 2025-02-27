using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : BaseUI
{
    [SerializeField] private Button easyButton;
    [SerializeField] private Button normalButton;
    [SerializeField] private Button hardButton;
    [SerializeField] private Button extremButton;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        easyButton.onClick.AddListener(OnClickEeasyButton);
        normalButton.onClick.AddListener(OnClickNormalButton);
        hardButton.onClick.AddListener(OnClickHardButton);
        extremButton.onClick.AddListener(OnClickExtremeButton);
    }

    protected override UIState GetUIstate()
  {
        return UIState.Stage;
  }


    public void OnClickEeasyButton()
    {
        uiManager.stageManager.ChangeDifficulty("Easy");
        GameManager.Instance.ChanageObstacleSpeed(0.0001f, 5f, 0.5f);
        uiManager.homeUI.stagePanel.SetActive(false);
    }

    public void OnClickNormalButton()
    {
        uiManager.stageManager.ChangeDifficulty("Normal");
        GameManager.Instance.ChanageObstacleSpeed(0.001f, 2f, 0.5f);
        uiManager.homeUI.stagePanel.SetActive(false);
    }


    public void OnClickHardButton()
    {
        uiManager.stageManager.ChangeDifficulty("Hard");
        GameManager.Instance.ChanageObstacleSpeed(0.01f, 1f, 0.5f);
        uiManager.homeUI.stagePanel.SetActive(false);
    }

    public void OnClickExtremeButton()
    {
        uiManager.stageManager.ChangeDifficulty("Extreme");
        GameManager.Instance.ChanageObstacleSpeed(0.1f, 1f, 0.8f);
        uiManager.homeUI.stagePanel.SetActive(false);
    }
  
}
