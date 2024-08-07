using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "HairShopSO")]
public class HairShopSO : ShopSO
{
    [SerializeField] private ItemHair[] itemsShopHair;
    public ItemHair[] ItemShopHair => itemsShopHair;
    [SerializeField] private Type type;
    public override Type Type => type;
    public override List<Item> Items
    {
        get
        {
            List<Item> itemList = new List<Item>();
            foreach (var itemHair in itemsShopHair)
            {
                Item newItem = new Item
                {
                    nameItem = itemHair.nameItem,
                    sprice = itemHair.sprice,
                    imageItem = itemHair.imageItem,
                    indexItem = (int) itemHair.hairtype,
                };
                itemList.Add(newItem);
            }
            return itemList;
        }
    }

}
[System.Serializable]public class ItemHair : Item
{
    public HairType hairtype;
} 