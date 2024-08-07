using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShopSO : ScriptableObject
{
    public abstract List<Item> Items { get; }
    public abstract Type Type { get; }
}
