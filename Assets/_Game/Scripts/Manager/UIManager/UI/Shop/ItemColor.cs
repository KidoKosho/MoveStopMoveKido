using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class ItemColor : MonoBehaviour
{
    [SerializeField] Image image;
    private ColorType colortype;
    public void UpdateUIItem(Material material,int i)
    {
        image.material = material;
        colortype = (ColorType)i;
    }
    public void DownButton()
    {
        Pref.ColorID = (int)colortype;
        LevelManager.Ins.player.ChangeColor(colortype);
        if(Pref.PantID == 0) LevelManager.Ins.player.ChangePant((PantType)Pref.PantID);
    }
}
