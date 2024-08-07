using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class CanvasShopSkin : UICanvas
{
    private UICanvas canvasbefore;
    public Tab[] Tabs;
    public Image[] TabButton;
    public Color InactiveTabBG, ActiveTagBG;
    public Vector2 InactiveTabButtonSize,ActiveTabButtonSize;
    public void ShowTab(int tabID)
    {
        for (int i = 0; i < Tabs.Length; i++)
        {
            Tabs[i].gameObject.SetActive(false);
        }
        Tabs[tabID].gameObject.SetActive(true);
        Tabs[tabID].Show();
        for (int i = 0; i < TabButton.Length; i++)
        {
            TabButton[i].color = InactiveTabBG;
            TabButton[i].rectTransform.sizeDelta = InactiveTabButtonSize;
        }
        TabButton[tabID].color = ActiveTagBG;
        TabButton[tabID].rectTransform.sizeDelta = ActiveTabButtonSize;
    }
    public UICanvas Canvasasbefor(UICanvas UIcanvas)
    {
        canvasbefore = UIcanvas;
        ShowTab(0);
        return this;
    }
    public void CloseButton()
    {
        Close(0);
        if (canvasbefore is CanvasMenu)
        {

            (canvasbefore as CanvasMenu).SetState(canvasbefore);
            (canvasbefore as CanvasMenu).ChangeMainMenu();
        }
    }
     public void ChangeCoinsMain()
    {
        (canvasbefore as CanvasMenu).UpdateCoins();
    }
}
