using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    public CameraFollow Camera;
    public void Start()
    {
        UIManager.Ins.OpenUI<CanvasMenu>();
        LevelManager.Ins.OnLoadLevel();
    }
    public void Update()
    {
    }
    public void ChangeCameraMainMenu()
    {
        Camera.ChangeCameraMainMenu();
    }
    public void ChangeCameraPlay()
    {
        Camera.ChangeCameraPlay();  
    }
}
