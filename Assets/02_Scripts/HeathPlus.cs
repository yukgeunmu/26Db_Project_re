using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeathPlus : ItemInstance, ITakeable
{
    public void Take()
    {
        GameManager.Instance.resourceController.ChangeHealth(30);
    }


}
