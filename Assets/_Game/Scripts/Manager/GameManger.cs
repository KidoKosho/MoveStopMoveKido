using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : Singleton<GameManger>
{ 
    [SerializeField] private CameraFollow Camera;
    private bool isplay = false;
    public bool Isplay => isplay;
    private void Awake()
    {
        if (!PlayerPrefs.HasKey(PrefConst.Hair_Player_ID + 0))
        {
            Pref.PantID = 0;
            Pref.WeaponID = 0;
            Pref.HairID = 0;
            Pref.Coins = 0;
            Pref.SetBool(PrefConst.Hair_Player_ID + 0, true);
            Pref.SetBool(PrefConst.Weapon_Player_ID + 0, true);
            Pref.SetBool(PrefConst.Pant_Player_ID + 0, true);
        }
    }
    public void Pause(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    public void ChangePlay(bool change)
    {
        isplay = change;
    }
    public void Start()
    {
        UIManager.Ins.OpenUI<CanvasMenu>().UpdateCoins();
        LevelManager.Ins.OnLoadLevel();
        ChangeCameraMainMenu();
    }
    public void ChangeCameraMainMenu()
    {
        Camera.ChangeCameraMainMenu();
    }
    public void ChangeCameraPlay()
    {
        Camera.ChangeCameraPlay();
    }
    public void ChangeCameraSkin()
    {
        Camera.ChangeCameraSkin();
    }
    public void ChangeCameraColor()
    {
        Camera.ChangeColorPlayer();
    }
}
