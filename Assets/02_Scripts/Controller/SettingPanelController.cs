using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanelController : MonoBehaviour
{
    public GameObject settingsPanel;

    public void OnClickOpenOptionPanel()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
}
