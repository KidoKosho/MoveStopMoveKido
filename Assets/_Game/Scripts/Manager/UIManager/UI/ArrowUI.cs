using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class ArrowUI : MonoBehaviour
{
    [SerializeField] private RectTransform arrow;
    [SerializeField] private ColorUISO colorUISO;
    [SerializeField] private UnityEngine.UI.Image imageArrow;
    [SerializeField] private UnityEngine.UI.Image imageLevel;
    [SerializeField] private TextMeshProUGUI textLevel;
    public void LoadUpdate(Vector3 screenPosition,Quaternion quaternion, Vector3 viewPosition,int level)
    {
        arrow.position = screenPosition;
        arrow.rotation = quaternion;
        //if (viewPosition.x > 0.5f) arrow.anchorMin = arrow.anchorMax = new Vector2(1, arrow.anchorMin.y);
        //else arrow.anchorMin = arrow.anchorMax = new Vector2(0, arrow.anchorMin.y);

        //if (viewPosition.y > 0.5f) arrow.anchorMin = arrow.anchorMax = new Vector2(arrow.anchorMin.x, 1);
        //else arrow.anchorMin = arrow.anchorMax = new Vector2(arrow.anchorMin.x, 0);
        LoadLevelUI(level);
    }
    public void OnInit(ColorType color,int level)
    {
        imageArrow.color = colorUISO.colorUI(color);
        imageLevel.color = colorUISO.colorUI(color);
        if (color == ColorType.Black) textLevel.color = colorUISO.colorUI(ColorType.None);
        else textLevel.color = colorUISO.colorUI(ColorType.Black);
        LoadLevelUI(level);
    }
    public void LoadLevelUI(int level)
    {
        textLevel.text = level.ToString();
    }
}
