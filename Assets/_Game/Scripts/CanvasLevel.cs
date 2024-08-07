using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class CanvasLevel : MonoBehaviour
{
    private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Image imgeUI;
    [SerializeField] private TextMeshProUGUI textUI;
    [SerializeField] private ColorUISO ColorUISO;
    [SerializeField] private TextMeshProUGUI nameplayer;

    void Update()
    {
        transform.position = target.position + offset;
    }
    public void OnInit(Transform target,ColorType color,int level,string name)
    {
        this.target = target;
        imgeUI.color =  ColorUISO.colorUI(color);
        nameplayer.text = name;
        nameplayer.color = imgeUI.color;
        if (color == ColorType.Black) textUI.color = ColorUISO.colorUI(ColorType.None);
        else textUI.color = ColorUISO.colorUI(ColorType.Black);
        LoadLevelUI(level);
        this.transform.rotation = Quaternion.Euler(60, 0, 0);
    }
    public void LoadLevelUI(int level)
    {
        textUI.text = level.ToString();
    }
}
