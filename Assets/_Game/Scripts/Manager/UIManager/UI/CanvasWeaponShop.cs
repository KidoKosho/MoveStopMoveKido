using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasWeaponShop : UICanvas
{
    // Start is called before the first frame update
    private UICanvas canvasbefore;
    [SerializeField] ShopSO ShopSO;
    [SerializeField] private Image itemImage;
    [SerializeField] private Button[] buttonsNextPrev;
    [SerializeField] private Button[] buttonSelectWeapom;
    [SerializeField] TextMeshProUGUI textName;
    [SerializeField] TextMeshProUGUI textPrice;
    int index;

    private void Awake()
    {
        
    }
    public UICanvas Canvasasbefor(UICanvas UIcanvas)
    {
        canvasbefore = UIcanvas;
        LoadUI();
        return this;
    }
    public void CloseButton()
    {
        Close(0);
        if(canvasbefore is CanvasMenu) {
            (canvasbefore as CanvasMenu).SetState(canvasbefore);
            (canvasbefore as CanvasMenu).ChangeMainMenu();
        }
        LevelManager.Ins.player.gameObject.SetActive(true);
    }
    public void LoadUI()
    {
        Debug.Log(ShopSO.Items.Count);
        for (int i = 0; i < ShopSO.Items.Count; i++)
        {
            if (!Pref.GetBool(PrefConst.Weapon_Player_ID + ShopSO.Items[i].indexItem))
            {
                LoadItemUI(ShopSO.Items[i], i);
                return;
            }
        }
        for (int i = 0; i < ShopSO.Items.Count; i++)
        {
            if (Pref.WeaponID == ShopSO.Items[i].indexItem)
            {
                LoadItemUI(ShopSO.Items[i], i);
                return;
            }
        }
    }
    public void LoadItemUI(Item item,int index)
    {
        this.index = index;
        itemImage.sprite = item.imageItem;
        textName.text = item.nameItem;
        for (int i = 0; i < buttonsNextPrev.Length; i++)
        {
            buttonsNextPrev[i].gameObject.SetActive(true);
        }
        if (index == 0) buttonsNextPrev[0].gameObject.SetActive(false);
        if (index ==  ShopSO.Items.Count - 1) buttonsNextPrev[1].gameObject.SetActive(false);
        LoadPrice();
    }
    public void NextItemUI()
    {
        if (index != ShopSO.Items.Count - 1) LoadItemUI(ShopSO.Items[index + 1], index + 1);
    }
    public void PrevItemUI()
    {
        if (index != 0) LoadItemUI(ShopSO.Items[index - 1], index - 1);
    }
    public void LoadPrice()
    {
        for(int i = 0;i < buttonSelectWeapom.Length;i++) buttonSelectWeapom[i].gameObject.SetActive(false);
        if(Pref.GetBool(PrefConst.Weapon_Player_ID + ShopSO.Items[index].indexItem))
        {
            if(Pref.WeaponID == ShopSO.Items[index].indexItem)
            {
                buttonSelectWeapom[2].gameObject.SetActive(true);
            }
            else
            {
                buttonSelectWeapom[1].gameObject.SetActive(true);
            }
        }
        else
        {
            buttonSelectWeapom[0].gameObject.SetActive(true);
            textPrice.text = ShopSO.Items[index].sprice.ToString();
        }
    }
    public void EquippedButton()
    {
        CloseButton();
    }
    public void SelectButton()
    {
        Pref.WeaponID = ShopSO.Items[index].indexItem;
        CloseButton();
    }
    public void BuyButton()
    {
        Debug.Log(Pref.Coins + " " + ShopSO.Items[index].sprice);
        if(Pref.Coins >= ShopSO.Items[index].sprice)
        {
            Pref.Coins -= ShopSO.Items[index].sprice;
            Pref.SetBool(PrefConst.Weapon_Player_ID + ShopSO.Items[index].indexItem, true);
            LoadPrice();
            (canvasbefore as CanvasMenu).UpdateCoins(); 
        }
    }
}
