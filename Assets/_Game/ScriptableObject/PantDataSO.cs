using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PantType
{
    None = 0,
    American = 1,
    Batman = 2,
    Onion = 3,
    Panther = 4,
    Pokemon = 5,
    Purpleveins = 6,
    Polkadots = 7,
    Rainbow = 8,
    Skull = 9
}
[CreateAssetMenu(menuName = "PantDataSO")]
public class PantDataSO : ScriptableObject
{
    [SerializeField] Material[] painmaterial;
    public int PainmaterialsCount => painmaterial.Length;
    public Material GetPant(PantType panttype)
    {
        return painmaterial[(int)panttype];
    }
}
