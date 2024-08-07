using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasSetting: UICanvas
{
    [SerializeField] GameObject[] buttons;
    public void SetState(UICanvas canvas)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
        if (canvas is CanvasMenu|| canvas is CanvasWin || canvas is CanvasLose)
        {
            buttons[2].gameObject.SetActive(true);
        }
        else if (canvas is CanvasGamePlay)
        {
            buttons[0].gameObject.SetActive(true);
            buttons[1].gameObject.SetActive(true);
        }
    }
    public void CloseButton()
    {
        Close(0);
    }
    public void MenuButton()
    {
        GameManger.Ins.Pause(false);
        GameManger.Ins.ChangePlay(false);
        LevelManager.Ins.OnDespawn();
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<CanvasMenu>().ChangeMainMenu();
        LevelManager.Ins.OnLoadLevel();
    }
    public void ContinueButton()
    {
        Close(0);
        GameManger.Ins.Pause(false);
    }
}
