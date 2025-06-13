using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpgradeScreen : MonoBehaviour
{
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private GameObject shopPanel;
    void Start()
    {
        if (DataManager.instance != null)
        {
            switch (DataManager.instance.activePanelOnLoad)
            {
                case DataManager.TargetPanel.Upgrade:
                    ShowUpgradePanel();
                    break;
                case DataManager.TargetPanel.Shop:
                    ShowShopPanel();
                    break;
                default:
                    ShowUpgradePanel();
                    break;
            }
        }
        else
        {
            ShowUpgradePanel();
        }
    }

    private void ShowUpgradePanel()
    {
        upgradePanel.SetActive(true);
        shopPanel.SetActive(false);
    }

    private void ShowShopPanel()
    {
        upgradePanel.SetActive(false);
        shopPanel.SetActive(true);
    }
}
