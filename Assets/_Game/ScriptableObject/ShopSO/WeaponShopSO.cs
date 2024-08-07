using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "WeaponShopSO")]
public class WeaponShopSO : ShopSO
{
    [SerializeField] private ItemWeapon[] itemsShopWeapon;
    public ItemWeapon[] ItemShopWeapon => itemsShopWeapon;
    [SerializeField] Type type;
    public override Type Type => type;
    public override List<Item> Items
    {
        get
        {
            List<Item> itemList = new List<Item>();
            foreach (var itemWeapon in itemsShopWeapon)
            {
                Item newItem = new Item
                {
                    nameItem = itemWeapon.nameItem,
                    sprice = itemWeapon.sprice,
                    imageItem = itemWeapon.imageItem,
                    indexItem = (int)itemWeapon.weapontype,
                };
                itemList.Add(newItem);
            }
            return itemList;
        }
    }
}
[System.Serializable]public class ItemWeapon : Item
{
    public WeaponType weapontype;
}