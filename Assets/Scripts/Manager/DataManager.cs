using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public PlayerScriptableObject playerData;


    [Header("---------------Current Collectible Of Player (Coin,Gem,Scroll)---------------")]

    public int currentMoney;
    public int currentGem;
    public int currentScrollPaper;


    //Seperate private stat in PlayerStats script and child stat in another script

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

    public int CurrentScrollPaper
    {
        get { return currentScrollPaper; }
        set
        {
            if (currentScrollPaper != value)
            {
                currentScrollPaper = value;
                if (UIForAll.instance != null)
                {
                    UIForAll.instance.currentScrollPaperDisplay.text = currentScrollPaper.ToString();
                }
            }
        }
    }


    [Header("---------------Current Skills Level Integer---------------")]
    public int currentDashLevel;
    public int currentThrowShurikenLevel;
    public int currentHealAndShieldLevel;
    public int currentCamouflageLevel;

    [Header("---------------Curent Skill Upgrade Data---------------")]

    public DashSkillScriptableObject currentDashData;
    public ThrowShurikenScriptableObject currentThrowShurikenData;
    public HealAndShieldScriptableObject currentHealAndShieldData;
    public CamouflageScriptableObject currentCamouflageData;


    [Header("---------------Skill Upgrade Data---------------")]
    public List<DashSkillScriptableObject> dashSkillData = new List<DashSkillScriptableObject>();
    public List<ThrowShurikenScriptableObject> throwShurikenData = new List<ThrowShurikenScriptableObject>();
    public List<HealAndShieldScriptableObject> healAndShieldData = new List<HealAndShieldScriptableObject>();
    public List<CamouflageScriptableObject> camouflageSkillData = new List<CamouflageScriptableObject>();



    void Awake()
    {

        SaveAndLoadManager.instance.DataManager = this;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if (!System.IO.File.Exists(Application.persistentDataPath + "/save" + ".save"))
        {
            currentMoney = playerData.Money;
            currentGem = playerData.Gem;
            currentScrollPaper = playerData.ScrollPaper;

            currentDashLevel = 0;
            currentCamouflageLevel = 0;
            currentThrowShurikenLevel = 0;
            currentHealAndShieldLevel = 0;
        }
        else
        {
            SaveAndLoadManager.LoadGame();
        }

        UpgradeCurrentSkillData();
    }

    void Start()
    {
        UIForAll.instance.currentMoneyDisplay.text = CurrentMoney.ToString();
        UIForAll.instance.currentGemDisplay.text = CurrentGem.ToString();
        UIForAll.instance.currentScrollPaperDisplay.text = CurrentScrollPaper.ToString();
    }

    public void UpgradeCurrentSkillData()
    {
        if (dashSkillData.Count > 0)
        {
            currentDashData = dashSkillData[Mathf.Clamp(currentDashLevel, 0, dashSkillData.Count - 1)];
            if (SkillUpgradeManager.instance != null)
            {
                SkillUpgradeManager.instance.currentDashData = currentDashData;
            }
        }
        if (throwShurikenData.Count > 0)
        {
            currentThrowShurikenData = throwShurikenData[Mathf.Clamp(currentThrowShurikenLevel, 0, throwShurikenData.Count - 1)];
            if (SkillUpgradeManager.instance != null)
            {
                SkillUpgradeManager.instance.currentThrowShurikenData = currentThrowShurikenData;

            }

        }
        if (healAndShieldData.Count > 0)
        {
            currentHealAndShieldData = healAndShieldData[Mathf.Clamp(currentHealAndShieldLevel, 0, healAndShieldData.Count - 1)];
            if (SkillUpgradeManager.instance != null)
            {
                SkillUpgradeManager.instance.currentHealAndShieldData = currentHealAndShieldData;

            }
        }
        if (camouflageSkillData.Count > 0)
        {
            currentCamouflageData = camouflageSkillData[Mathf.Clamp(currentCamouflageLevel, 0, camouflageSkillData.Count - 1)];
            if (SkillUpgradeManager.instance != null)
            {
                SkillUpgradeManager.instance.currentCamouflageData = currentCamouflageData;

            }
        }
    }




    #region Save And Load Player Data

    public void Save(ref PlayerSaveData data)
    {
        data.Gem = CurrentGem;
        data.Coin = CurrentMoney;
        data.ScrollPaper = CurrentScrollPaper;
        data.DashLevel = currentDashLevel;
        data.ThrowShurikenLevel = currentThrowShurikenLevel;
        data.HealAndShieldLevel = currentHealAndShieldLevel;
        data.CamouflageLevel = currentCamouflageLevel;

    }

    public void Load(PlayerSaveData data)
    {
        CurrentGem = data.Gem;
        CurrentMoney = data.Coin;
        CurrentScrollPaper = data.ScrollPaper;
        currentDashLevel = data.DashLevel;
        currentThrowShurikenLevel = data.ThrowShurikenLevel;
        currentHealAndShieldLevel = data.HealAndShieldLevel;
        currentCamouflageLevel = data.CamouflageLevel;

    }

    [System.Serializable]
    public struct PlayerSaveData
    {
        public int Coin;
        public int Gem;
        public int ScrollPaper;
        public int DashLevel;
        public int ThrowShurikenLevel;
        public int HealAndShieldLevel;
        public int CamouflageLevel;
    }

    #endregion
}
