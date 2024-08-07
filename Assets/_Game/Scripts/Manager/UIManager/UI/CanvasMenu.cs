using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasMenu : UICanvas
{
    [SerializeField] GameObject[] buttons;
    [SerializeField] TextMeshProUGUI textCoins;
    public void SetState(UICanvas canvas)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(true);
        }
        if (!(canvas is CanvasMenu))
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i != 0) buttons[i].gameObject.SetActive(false);
            }
        }
    }
    public void LoadActiveButon()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(true);
        }
        GameManger.Ins.ChangeCameraMainMenu();
        UpdateCoins();
    }
    public void PlayGameButton()
    {
        Close(0);
        UIManager.Ins.OpenUI<CanvasGamePlay>();
        GameManger.Ins.ChangePlay(true);
        GameManger.Ins.ChangeCameraPlay();
    }
    public void SettingButton()
    {
        UIManager.Ins.OpenUI<CanvasSetting>().SetState(this);
    }
    public void WeaponButton()
    {
        UICanvas uicanvas = UIManager.Ins.OpenUI<CanvasWeaponShop>().Canvasasbefor(this);
        SetState(uicanvas);
        LevelManager.Ins.player.gameObject.SetActive(false);
        GameManger.Ins.ChangeCameraSkin();
    }
    public void ShopSkinButton()
    {
        UICanvas uicanvas = UIManager.Ins.OpenUI<CanvasShopSkin>().Canvasasbefor(this);
        SetState(uicanvas);
        GameManger.Ins.ChangeCameraSkin();
    }
    public void ChangeMainMenu()
    {
        UpdateCoins();
        ChangePlayer();
        GameManger.Ins.ChangeCameraMainMenu();
    }
    public void UpdateCoins()
    {
        textCoins.text = Pref.Coins.ToString(); 
    }
    public void ChangeColor()
    {
        UICanvas uicanvas = UIManager.Ins.OpenUI<CanvasColor>().Canvasasbefor(this);
        SetState(uicanvas);
        GameManger.Ins.ChangeCameraColor();
    }
    public void ChangePlayer()
    {
        LevelManager.Ins.player.OnInit();
    }
}
