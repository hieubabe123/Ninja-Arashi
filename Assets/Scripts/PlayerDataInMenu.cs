using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataInMenu : MonoBehaviour
{
    [Header("---------------Coin &  Gem -------------------")]
    [SerializeField] private PlayerScriptableObject playerData;
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

    private void Awake()
    {
        CurrentMoney = playerData.Money;
        CurrentGem = playerData.Gem;
    }

    private void Start()
    {
        UIForAll.instance.currentMoneyDisplay.text = CurrentMoney.ToString();
        UIForAll.instance.currentGemDisplay.text = CurrentGem.ToString();
    }

}
