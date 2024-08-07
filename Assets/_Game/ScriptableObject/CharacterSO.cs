using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSO : GameUnit
{
    public WeaponDataSO weaponDataSO;
    public ColorDataSO colorDataSO;
    public HairDataSO hairDataSO;
    public PantDataSO pantDataSO;
    public new Renderer renderer;
    public NameShopSO nameShopSO;
    public ColorType color;
    public WeaponType Weapon;
    public HairType hair;
    public PantType pant;
    public int indexname;
    public void ChangeColor(ColorType colorType)
    {
        color = colorType;
        renderer.material = colorDataSO.GetMat(colorType);
    }
    public void RandomWeapon()
    {
        int index = Random.Range(0, weaponDataSO.weaponCount);
        Weapon = (WeaponType)index;
    }
    public void RandomMaterial()
    {
        int index = Random.Range(0, colorDataSO.materialsCount);
        color = (ColorType)index;
    }
    public void RandomHair()
    {
        int index = Random.Range(0, hairDataSO.hairsCount);
        hair = (HairType)index;
    }
    public void RandomPant() 
    { 
        int index = Random.Range(0,pantDataSO.PainmaterialsCount);
        pant = (PantType)index;
    }
    public void RandomName()
    {
        indexname = nameShopSO.RandomName();
    }
}
