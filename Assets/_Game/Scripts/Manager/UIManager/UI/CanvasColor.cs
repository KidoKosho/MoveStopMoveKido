using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasColor : UICanvas
{
    [SerializeField] private ColorShopSO colorSO;
    [SerializeField] private ItemColor ItemColorPrefabs;
    [SerializeField] Transform Transform;
    private UICanvas canvasbefore;
    public UICanvas Canvasasbefor(UICanvas UIcanvas)
    {
        canvasbefore = UIcanvas;
        gameObject.SetActive(true);
        return this;
    }
    public void Awake()
    {
        for(int i =0;i < colorSO.materialsCount; ++i)
        {
            ItemColor itemcolor = Instantiate(ItemColorPrefabs, Transform);
            itemcolor.UpdateUIItem(colorSO.GetMat((ColorType)i),i);
        }
    }
    public void Close()
    {
        Close(0);
        if (canvasbefore is CanvasMenu)
        {
            (canvasbefore as CanvasMenu).SetState(canvasbefore);
            (canvasbefore as CanvasMenu).ChangeMainMenu();
        }
    }
}
