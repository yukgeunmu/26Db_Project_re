using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeathPlus : ItemInstance, ITakeable
{
    public void Take()
    {
        if(GameManager.Instance.resourceController.CurrentHealth >= 100) return;   
        else GameManager.Instance.resourceController.ChangeHealth(30);
    }


}
