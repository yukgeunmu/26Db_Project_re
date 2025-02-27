using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCoinItem : ItemInstance, ITakeable
{
    public void Take()
    {
        GameManager.Instance.AddScore(15);
    }

}
