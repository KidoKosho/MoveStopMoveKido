using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    [SerializeField] private bool isDestroyOnclose = false;
    private void Awake()
    {
        RectTransform rect = GetComponent<RectTransform>();
        float ratio = (float)Screen.width / (float)Screen.height;
        if (ratio > 2.1f)
        {
            Vector2 leftBottom = rect.offsetMin;
            Vector2 rightTop = rect.offsetMax;

            leftBottom.y = 0f;
            rightTop.y = -100f;

            rect.offsetMax = leftBottom;
            rect.offsetMin = rightTop;
        }
    }
    // goi truoc khi canvas dc active
    public virtual void Setup()
    {

    }
    //goi sau khi duoc active
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }
    //tat canvas sau time(s)
    public virtual void Close(float time)
    {
        Invoke(nameof(CloseDirectly), time);
    }
    //tat canvas truc tiep
    public virtual void CloseDirectly()
    {
        if (isDestroyOnclose)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
