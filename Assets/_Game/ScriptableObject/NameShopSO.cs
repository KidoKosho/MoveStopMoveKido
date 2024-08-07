using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "NameShopSO")]
public class NameShopSO : ScriptableObject
{
    [SerializeField] string[] namecharacters;
    public string GetString(int i)
    {
        return namecharacters[i];
    }
    public int RandomName()
    {
        return Random.Range(1, namecharacters.Length);
    }
}
