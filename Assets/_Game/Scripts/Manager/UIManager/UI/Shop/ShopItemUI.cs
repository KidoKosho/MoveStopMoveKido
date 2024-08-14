using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public TextMeshProUGUI textPrice;
    public TextMeshProUGUI textName;
    public Image imageItem;
    public Image imageLock;
    public Image imageSelect;
    public Button btn;
    public Image BG;
    bool isUnlocked;
    [SerializeField] private Image equippedImage;
    public void UpdateIteamUI(Item item,string playerid)
    {
        if(item.indexItem == 0) imageItem.gameObject.SetActive(false);
        isUnlocked = Pref.GetBool(playerid + item.indexItem);
        if (isUnlocked)
        {
            imageLock.gameObject.SetActive(false);
            if (item.indexItem == PlayerPrefs.GetInt(playerid))
            {
                equippedImage.gameObject.SetActive(true);
            }
            else equippedImage.gameObject.SetActive(false);
        }
        else
        {
        }
    }
    public void OniIntItemUI(Item item, string playerid)
    {
        if (item == null) return;
        if (imageItem) imageItem.sprite = item.imageItem;
        if (textName) textName.text = item.nameItem;
        equippedImage.gameObject.SetActive(false);
        imageSelect.gameObject.SetActive(false);
        imageLock.gameObject.SetActive(true);
        UpdateIteamUI(item, playerid);
    }
}
