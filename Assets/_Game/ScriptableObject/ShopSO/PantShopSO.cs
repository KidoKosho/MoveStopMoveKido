using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "PantShopSO")]
public class PantShopSO : ShopSO
{
    [SerializeField] private ItemPant[] itemsShopPant;
    public ItemPant[] ItemShopPants => itemsShopPant;
    [SerializeField] private Type type;
    public override Type Type  => type;
    public override List<Item> Items
    {
        get
        {
            List<Item> itemList = new List<Item>();
            foreach (var itemHair in itemsShopPant)
            {
                Item newItem = new Item
                {
                    nameItem = itemHair.nameItem,
                    sprice = itemHair.sprice,
                    imageItem = itemHair.imageItem,
                    indexItem = (int) itemHair.panttype,
                };
                itemList.Add(newItem);
            }
            return itemList;
        }
    }
}
[System.Serializable] public class ItemPant : Item
{
    public PantType panttype;
}