using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Item Data", order = 0)]
public class ItemDataSerializer : ScriptableObject
{
    public List<ItemData> ItemData;
}

[Serializable]
public struct ItemData
{
    public string ItemName;
    public OID OID;
    public Item Item;
    public Sprite ItemSprite;
    public int WarmCount;
}