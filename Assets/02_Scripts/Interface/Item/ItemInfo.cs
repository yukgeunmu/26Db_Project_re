using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemInfo
{
    /// <summary>
    /// ID
    /// </summary>
    public int key;

    /// <summary>
    /// �̸�
    /// </summary>
    public string Name;

    /// <summary>
    /// ���
    /// </summary>
    //public DesignEnums.Grade Grade;

    /// <summary>
    /// ������
    /// </summary>
    public int Durability;

    /// <summary>
    /// �ɼ�
    /// </summary>
    public List<int> AvailableOptions;

    /// <summary>
    /// ����
    /// </summary>
    public string Description;
}

