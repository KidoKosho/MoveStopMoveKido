using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponType
{
    Arrpw = 0,
    Axe_0 = 1,
    Axe_1 = 2,
    Boomerang = 3,
    Candy_0 = 4,
    Candy_1 = 5,
    Candy_2 = 6,
    Hammer = 7,
    Knife = 8,
    Uzi = 9,
    Candy_4 =10,
}
[CreateAssetMenu(menuName = "WeaponDataSO")]
public class WeaponDataSO : ScriptableObject
{
    [SerializeField] Weapon[] weapons;
    public int weaponCount => weapons.Length;
    public Weapon GetWeapon(WeaponType Weapon)
    {
        return weapons[(int)Weapon];
    }
    
}
