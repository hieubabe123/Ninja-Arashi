using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIForAll : MonoBehaviour
{
    public static UIForAll instance;

    [Header("---------------Current Stat Displays---------------")]
    public TMP_Text currentLifeCountDisplay;
    public TMP_Text currentMoneyDisplay;
    public TMP_Text currentGemDisplay;
    public TMP_Text currentScrollPaperDisplay;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
