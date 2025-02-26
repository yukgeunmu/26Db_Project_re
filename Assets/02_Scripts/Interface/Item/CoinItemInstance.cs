using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItemInstance : ItemInstance, ITakeable
{
    public void Take()
    {
        GameManager.Instance.AddCoin(1);
    }
}
