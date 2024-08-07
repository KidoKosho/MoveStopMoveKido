using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICanvasBought : UICanvas
{
    [SerializeField] TextMeshProUGUI textBuy;

    public void Buy(bool check)
    {
        if (check)
        {
            textBuy.text = "You have successfully purchased it";
        }
        else
        {
            textBuy.text = "you don't have enough money to buy it";
        }
    }
}
