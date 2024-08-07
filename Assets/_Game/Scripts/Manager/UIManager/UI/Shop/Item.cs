using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Item
{
    public string nameItem;
    public int sprice;
    public Sprite imageItem;
    public int indexItem { get; set; }
}

public enum Type
{
    PantType = 0,
    WeaponType = 1,
    HairType = 2,
}