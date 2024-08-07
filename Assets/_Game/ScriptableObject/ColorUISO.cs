using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName = "ColorUISO")]
public class ColorUISO : ScriptableObject
{
    [SerializeField]private ColorUI[] colorUIs;
    public UnityEngine.Color colorUI(ColorType colorType)
    {
        return colorUIs[(int)colorType].color;
    }
}
[System.Serializable]public class ColorUI
{
    public UnityEngine.Color color;
    public ColorType colorType;
}
