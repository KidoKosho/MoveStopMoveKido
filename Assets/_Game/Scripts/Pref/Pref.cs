using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Pref 
{
    public static int Coins
    {
        set => PlayerPrefs.SetInt(PrefConst.COIN_KEY,value);
        get => PlayerPrefs.GetInt(PrefConst.COIN_KEY);
    }
    public static int WeaponID
    {
        set => PlayerPrefs.SetInt(PrefConst.Weapon_Player_ID, value);
        get => PlayerPrefs.GetInt(PrefConst.Weapon_Player_ID);
    }
    public static int PantID
    {
        set => PlayerPrefs.SetInt(PrefConst.Pant_Player_ID, value);
        get => PlayerPrefs.GetInt(PrefConst.Pant_Player_ID);
    } 
    public static int HairID
    {
        set => PlayerPrefs.SetInt(PrefConst.Hair_Player_ID, value);
        get => PlayerPrefs.GetInt(PrefConst.Hair_Player_ID);
    }
    public static int ColorID
    {
        set => PlayerPrefs.SetInt(PrefConst.Color_Player_ID, value);
        get => PlayerPrefs.GetInt(PrefConst.Color_Player_ID);
    }
    public static void SetBool(string key,bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt(key, 1);
        }
        else
        {
            PlayerPrefs.SetInt(key, 0);
        }
    }
    public static bool GetBool(string key)
    {
        if (!PlayerPrefs.HasKey(key)) return false;
        return PlayerPrefs.GetInt(key) == 1;
    }
}
