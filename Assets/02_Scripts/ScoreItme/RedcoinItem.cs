using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecdoinItem : ItemInstance, ITakeable
{
    public void Take()
    {
        GameManager.Instance.AddScore(10);
    }

}
