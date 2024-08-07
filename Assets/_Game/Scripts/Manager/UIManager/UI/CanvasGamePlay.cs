using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] private TextMeshProUGUI AliveText;
    private void Update()
    {
        ChangeTextAlive();
    }
    public void ChangeTextAlive()
    {
        AliveText.text = "Alive : " + LevelManager.Ins.Alive.ToString();
    }
    public void SettingButton()
    {
        GameManger.Ins.Pause(true);
        UIManager.Ins.OpenUI<CanvasSetting>().SetState(this);
    }
}
