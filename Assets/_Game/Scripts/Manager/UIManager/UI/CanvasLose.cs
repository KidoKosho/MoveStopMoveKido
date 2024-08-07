using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanvasLose : UICanvas
{
    [SerializeField] private Image[] imageStar;
    [SerializeField] private TextMeshProUGUI textCoins;
    [SerializeField] private Button ButtonMenu;
    private float delay = 2f;
    int coins = 0;
    int staractive = 0;
    public void SettingButton()
    {
        UIManager.Ins.OpenUI<CanvasSetting>().SetState(this);
    }
    public void MainMenuButton()
    {
        UIManager.Ins.CloseAll();
        LevelManager.Ins.OnReset();
        UIManager.Ins.OpenUI<CanvasMenu>().LoadActiveButon();
    }
    public void ShowCanvas(int characterdie,float star)
    {
        star = star > 1f ? 1f : star;
        staractive = (int)(star*imageStar.Length);
        coins = characterdie;
        Pref.Coins += coins;
        textCoins.text = 0.ToString(); 
        for (int i = 0; i < imageStar.Length; ++i)
        {
            imageStar[i].gameObject.SetActive(false);
        }
        ButtonMenu.gameObject.SetActive(false);
        StartCoroutine(ShowStarsCoroutine(delay/4));
        StartCoroutine(StartCoinsCoroutine(delay/(coins+2)));
        StartCoroutine(ShowButtonMenu(delay));
    }

    private IEnumerator ShowStarsCoroutine(float delay)
    {
       for(int i =0;i< staractive; ++i)
        {
            yield return new WaitForSeconds(delay);
            imageStar[i].gameObject.SetActive(true);
        }
    }
    private IEnumerator ShowButtonMenu(float delay)
    {
        yield return new WaitForSeconds(delay);
        ButtonMenu.gameObject.SetActive(true);

    }
    private IEnumerator StartCoinsCoroutine(float delay)
    {
        for (int i = 1; i <= coins; ++i)
        {
            textCoins.text = i.ToString();
            yield return new WaitForSeconds(delay);
        }
    }
}
