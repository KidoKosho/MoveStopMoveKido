using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HairType
{
    None = 0,
    Arrow = 1,
    Beard = 2,
    Crown = 3,
    Ear = 4,
    Hat = 5,
    Hat_Cap = 6,
    Hat_Yellow = 7,
    Headphone = 8,
    Hom = 9,
    CowBoy=10,
}

[CreateAssetMenu(menuName = "HairDataSO")]
public class HairDataSO : ScriptableObject
{
    [SerializeField] GameObject[] hairs;
    public int hairsCount => hairs.Length;
    public GameObject GetHair(HairType Hair)
    { 
        return hairs[(int)Hair];
    }
}
