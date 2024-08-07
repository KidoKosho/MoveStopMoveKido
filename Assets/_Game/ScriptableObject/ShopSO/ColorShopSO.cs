using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ColorShopSO")]
public class ColorShopSO : ScriptableObject
{
    [SerializeField] ColorMaterial[] colormaterials;
    public int materialsCount => colormaterials.Length;
    public ColorMaterial[] Colormaterials => colormaterials;
    public Material GetMat(ColorType color)
    {
        return colormaterials[(int)color].material;
    }
}
[System.Serializable] public class ColorMaterial
{
    public Material material;
    public ColorType color;
}