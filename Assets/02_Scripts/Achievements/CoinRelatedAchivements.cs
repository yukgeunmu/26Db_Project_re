using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRelatedAchivements : MonoBehaviour, IAchivement
{
    public bool TenCoin { get; private set; } = false;
    public bool HundreadCoin { get; private set; } = false;
    public bool ThousandCoin { get; private set; } = false;

    public void UpdateIsAchieved()
    {
        if(GameManager.Instance.Coin >= 10) TenCoin = true;
        if(GameManager.Instance.Coin >= 100) HundreadCoin = true;
        if(GameManager.Instance.Coin >= 1000) ThousandCoin = true;
    }
}