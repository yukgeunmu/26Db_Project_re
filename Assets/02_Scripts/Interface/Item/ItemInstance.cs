using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInstance : MonoBehaviour
{
    public ItemInfo ItemData { get; private set; } // �������� ���� ������

    //public List<ItemOption> Options { get; private set; } // 3���� ���� �ɼ�
    //public int Durability { get; private set; } // ������

    //private GachaSystem _gachaSystem;
    //public ItemInstance(ItemInfo itemData, GachaSystem gachaSystem)
    //{
    //    _gachaSystem = gachaSystem;

    //    ItemData = itemData;
    //    Durability = itemData.Durability;
    //    Options = new List<ItemOption>();

    //    AddRandomOption();
    //}

    //public void AddRandomOption()
    //{
    //    if (Options.Count >= 3)
    //        return;

    //    Options.Add(_gachaSystem.GetRandomOptions(ItemData));
    //}

    //public void UpdateRandomOption(int index)
    //{
    //    if (Options.Count <= index)
    //    {
    //        AddRandomOption();
    //        return;
    //    }

    //    Options[index] = (_gachaSystem.GetRandomOptions(ItemData));
    //}

    //// ������ ����
    //public void ReduceDurability(int amount)
    //{
    //    Durability = Mathf.Max(0, Durability - amount);
    //    if (Durability == 0)
    //    {
    //        OnBroken();
    //    }
    //}

    //// ������ 0 ó��
    //private void OnBroken()
    //{
    //    Debug.Log($"{ItemData.Name} is broken!");
    //}

    //public override string ToString()
    //{
    //    if (ItemData == null)
    //        return "";

    //    return ItemData.Name;
    //}
}
