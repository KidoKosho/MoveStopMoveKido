using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Tab : MonoBehaviour
{
    [SerializeField] CanvasShopSkin CanvasShop;
    [SerializeField] ShopSO ShopSO;
    private List<Item> shops = new List<Item>();
    [SerializeField] ShopItemUI ItemUIPrefabs;
    [SerializeField] Transform Transform;
    private string namePref;
    private int indexcurrent;
    [SerializeField] private Button[] ButtonShop;
    [SerializeField] private TextMeshProUGUI Textprice;
    private List<ShopItemUI> ShopItems = new List<ShopItemUI>();
    private int currentItem = 0;

    public void Awake()
    {
        if (ShopSO.Type == Type.PantType)
        {
            namePref = PrefConst.Pant_Player_ID;
        }
        else if (ShopSO.Type == Type.WeaponType)
        {
            namePref = PrefConst.Weapon_Player_ID;
        }
        else
        {
            namePref = PrefConst.Hair_Player_ID;
        }
        currentItem = PlayerPrefs.GetInt(namePref);
        for (int i = 0; i < ShopSO.Items.Count; i++)
        {
            ShopItemUI itemClone = Instantiate(ItemUIPrefabs, Transform);
            if (ShopSO.Items[i] != null)
            {
                itemClone.OniIntItemUI(ShopSO.Items[i], namePref);
                shops.Add(ShopSO.Items[i]);
                ShopItems.Add(itemClone);
                // Tạo một bản sao của `i` cho mỗi listener
                int index = i;
                if (itemClone.btn)
                {
                    itemClone.btn.onClick.RemoveAllListeners();
                    itemClone.btn.onClick.AddListener(() => ItemEvent(shops[index],index));//luu index khi bam vao
                }
            }
        }
        for (int i = 0; i < ButtonShop.Length; i++)
        {
            ButtonShop[i].gameObject.SetActive(false);
        }
    }
    public void Show()//chay du lieu shop
    {
        for (int i = 0; i < ShopSO.Items.Count; i++)
        {
            ShopItems[i].imageSelect.gameObject.SetActive(false);
            if (ShopSO.Items[i].indexItem == PlayerPrefs.GetInt(namePref))
            {
                ShopItems[i].imageSelect.gameObject.SetActive(true);
                indexcurrent = i;
            }
        }
    }

    public void ItemEvent(Item item ,int index)
    {
        ShopItems[indexcurrent].UpdateIteamUI(ShopSO.Items[indexcurrent], namePref); // Load lai Item
        ShopItems[indexcurrent].imageSelect.gameObject.SetActive(false);//tat vi tri chon khi trc
        indexcurrent = index;
        ShopItems[indexcurrent].imageSelect.gameObject.SetActive(true);//bat select vi tri dang chon
        ChangeButtonShop(item);
        ChangeSkinShop(item);
    }
    public void SelectButton()// deo vao
    {
        PlayerPrefs.SetInt(namePref, ShopSO.Items[indexcurrent].indexItem);//luu gia tri
        ShopItems[currentItem].UpdateIteamUI(ShopSO.Items[currentItem], namePref);//update Item deo trc do
        ShopItems[indexcurrent].UpdateIteamUI(ShopSO.Items[indexcurrent], namePref);//update Item muon deo
        currentItem = indexcurrent; //luu gia tri dang deo
        ChangeButtonShop(ShopSO.Items[indexcurrent]);//thay doi hinh anh Shop
    }
    public void UnequipButton()//thao skin cho nhan vat
    {
        if(currentItem == 0) return;//neu dang deo nhan vat la vi tri dau thi xoa
        PlayerPrefs.SetInt(namePref, 0);//luu gia tri
        currentItem = 0;//cho gia tri nhan vat dang deo la 0
        ShopItems[indexcurrent].UpdateIteamUI(ShopSO.Items[indexcurrent], namePref);//LoadlaiUI
        ChangeButtonShop(ShopSO.Items[indexcurrent]);
        ChangeSkinShop(ShopSO.Items[0]);
        ShopItems[0].UpdateIteamUI(ShopSO.Items[0], namePref);//LoadUi dau tien
    }
    public void BuyButton()
    {
        Debug.Log("buy");
        if (Pref.Coins >= ShopSO.Items[indexcurrent].sprice)
        {
            Pref.Coins -= ShopSO.Items[indexcurrent].sprice;
            Pref.SetBool(namePref + ShopSO.Items[indexcurrent].indexItem, true);
            SelectButton();
            UIManager.Ins.OpenUI<UICanvasBought>().Buy(true);
            CanvasShop.ChangeCoinsMain();
        }
        else
        {
            Debug.Log("khong du tien ban oi");
            UIManager.Ins.OpenUI<UICanvasBought>().Buy(false);
        }
    }
    public void ChangeButtonShop(Item item)//hien gia button mua ban
    {
        for (int i = 0; i < ButtonShop.Length; ++i) ButtonShop[i].gameObject.SetActive(false);
        if (Pref.GetBool(namePref + item.indexItem))
        {
            if (item.indexItem == PlayerPrefs.GetInt(namePref))
            {
                ButtonShop[0].gameObject.SetActive(true);
            }
            else
            {
                ButtonShop[1].gameObject.SetActive(true);
            }
        }
        else
        {
            Textprice.text = item.sprice.ToString();
            ButtonShop[2].gameObject.SetActive(true);
        }
    }
    public void ChangeSkinShop(Item item)// thay doi hinh anh nhan vat
    {
        if (ShopSO.Type == Type.PantType) LevelManager.Ins.player.ChangePant((PantType)item.indexItem);
        else if (ShopSO.Type == Type.HairType) LevelManager.Ins.player.ChangeHair((HairType)item.indexItem);
        else if (ShopSO.Type == Type.WeaponType) LevelManager.Ins.player.ChangeWeap((WeaponType)item.indexItem);
    }
}
