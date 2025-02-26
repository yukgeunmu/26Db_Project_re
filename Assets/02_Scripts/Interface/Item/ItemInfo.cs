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
    /// 이름
    /// </summary>
    public string Name;

    /// <summary>
    /// 등급
    /// </summary>
    //public DesignEnums.Grade Grade;

    /// <summary>
    /// 내구도
    /// </summary>
    public int Durability;

    /// <summary>
    /// 옵션
    /// </summary>
    public List<int> AvailableOptions;

    /// <summary>
    /// 설명
    /// </summary>
    public string Description;
}

