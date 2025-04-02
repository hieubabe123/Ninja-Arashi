using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataInMenu : MonoBehaviour
{

    private int currentMoney;
    private int currentGem;

    public int CurrentGem
    {
        get { return currentGem; }
        set
        {
            if (currentGem != value)
            {
                currentGem = value;
                if (UIForAll.instance != null)
                {
                    UIForAll.instance.currentGemDisplay.text = currentGem.ToString();
                }
            }
        }
    }

    public int CurrentMoney
    {
        get { return currentMoney; }
        set
        {
            if (currentMoney != value)
            {
                currentMoney = value;
                if (UIForAll.instance != null)
                {
                    UIForAll.instance.currentMoneyDisplay.text = currentMoney.ToString();
                }
            }
        }
    }

    private void Start()
    {
        UIForAll.instance.currentMoneyDisplay.text = CurrentMoney.ToString();
        UIForAll.instance.currentGemDisplay.text = CurrentGem.ToString();
    }




}
