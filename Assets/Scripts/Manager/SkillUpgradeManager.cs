using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillUpgradeManager : MonoBehaviour
{
    public static SkillUpgradeManager instance;

    [Header("---------------Skill Upgrade Data---------------")]
    public List<DashSkillScriptableObject> dashSkillData;
    public List<ThrowShurikenScriptableObject> throwShurikenData;
    public List<HealAndShieldScriptableObject> healAndShieldData;
    public List<CamouflageScriptableObject> camouflageSkillData;

    [Header("---------------Current Skill Data---------------")]
    public DashSkillScriptableObject currentDashData;
    public ThrowShurikenScriptableObject currentThrowShurikenData;
    public HealAndShieldScriptableObject currentHealAndShieldData;
    public CamouflageScriptableObject currentCamouflageData;

    [Header("---------------Current Skills Level Integer---------------")]
    public int currentDashLevel;
    public int currentThrowShurikenLevel;
    public int currentHealAndShieldLevel;
    public int currentCamouflageLevel;

    [Header("---------------UI For Level Upgrade---------------")]
    public List<GameObject> dashSkillCircles;
    public List<GameObject> throwShurikenCircles;
    public List<GameObject> healAndShieldCircles;
    public List<GameObject> camouflageCircles;

    [Header("---------------Skill Descriptions---------------")]
    public List<GameObject> dashSkillDescriptions;
    public List<GameObject> throwShurikenDescription;
    public List<GameObject> healAndShieldDescription;
    public List<GameObject> camouflageDescription;

    [Header("---------------Skill Button Text---------------")]
    public TMP_Text dashSkillButtonText;
    public TMP_Text throwShurikenButtonText;
    public TMP_Text healAndShieldButtonText;
    public TMP_Text camouflageButtonText;

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
    void Start()
    {
        UpdateSkillButtonTextColors();

        dashSkillData = DataManager.instance.dashSkillData;
        throwShurikenData = DataManager.instance.throwShurikenData;
        healAndShieldData = DataManager.instance.healAndShieldData;
        camouflageSkillData = DataManager.instance.camouflageSkillData;

        currentDashData = DataManager.instance.currentDashData;
        currentThrowShurikenData = DataManager.instance.currentThrowShurikenData;
        currentHealAndShieldData = DataManager.instance.currentHealAndShieldData;
        currentCamouflageData = DataManager.instance.currentCamouflageData;

        currentDashLevel = DataManager.instance.currentDashLevel;
        currentHealAndShieldLevel = DataManager.instance.currentHealAndShieldLevel;
        currentThrowShurikenLevel = DataManager.instance.currentThrowShurikenLevel;
        currentCamouflageLevel = DataManager.instance.currentCamouflageLevel;

        UpdateSkillButtonTextColors();

        UpdateAllSkillCircles();
        UpdateAllSkillDescriptions();

    }
    public void OnClickUpgradeSkill(string skillType)
    {
        switch (skillType)
        {
            case "Dash Skill":
                UpgradeSkill(ref currentDashLevel, dashSkillData, dashSkillCircles, dashSkillButtonText);
                break;
            case "Throw Shuriken":
                UpgradeSkill(ref currentThrowShurikenLevel, throwShurikenData, throwShurikenCircles, throwShurikenButtonText);
                break;
            case "Heal And Shield":
                UpgradeSkill(ref currentHealAndShieldLevel, healAndShieldData, healAndShieldCircles, healAndShieldButtonText);
                break;
            case "Camouflage Skill":
                UpgradeSkill(ref currentCamouflageLevel, camouflageSkillData, camouflageCircles, camouflageButtonText);
                break;
        }
        SyncDataToDataManager();
        DataManager.instance.UpgradeCurrentSkillData();
        UpdateAllSkillDescriptions();

    }

    private void UpdateButtonTextColor<T>(TMP_Text buttonText, int currentLevel, List<T> skillDataList) where T : SkillUpgradeScriptableObject
    {
        if (currentLevel < skillDataList.Count - 1)
        {
            T nextSkill = skillDataList[currentLevel + 1];
            if (DataManager.instance.currentMoney >= nextSkill.MoneyToUpgrade && DataManager.instance.currentGem >= nextSkill.GemToUpgrade)
            {
                buttonText.color = Color.white;
            }
            else
            {
                buttonText.color = Color.red;
            }
        }
        else
        {
            buttonText.color = Color.gray;
        }
    }



    private void UpgradeSkill<T>(ref int currentLevel, List<T> skillDataList, List<GameObject> skillCircles, TMP_Text buttonText) where T : SkillUpgradeScriptableObject
    {
        if (currentLevel >= skillDataList.Count - 1)
        {
            return;
        }

        T nextSkill = skillDataList[currentLevel + 1];
        if (DataManager.instance.currentMoney >= nextSkill.MoneyToUpgrade && DataManager.instance.currentGem >= nextSkill.GemToUpgrade)
        {
            DataManager.instance.CurrentMoney -= nextSkill.MoneyToUpgrade;
            DataManager.instance.CurrentGem -= nextSkill.GemToUpgrade;
            UIForAll.instance.currentMoneyDisplay.text = DataManager.instance.CurrentMoney.ToString();
            UIForAll.instance.currentGemDisplay.text = DataManager.instance.CurrentGem.ToString();

            currentLevel++;
        }
        else
        {
            return;
        }
        SaveAndLoadManager.SaveGame();
        UpdateSkillButtonTextColors();
        UpdateAllSkillCircles();
        UpdateAllSkillDescriptions();

    }

    private void UpdateSkillButtonTextColors()
    {
        UpdateButtonTextColor(dashSkillButtonText, currentDashLevel, dashSkillData);
        UpdateButtonTextColor(throwShurikenButtonText, currentThrowShurikenLevel, throwShurikenData);
        UpdateButtonTextColor(healAndShieldButtonText, currentHealAndShieldLevel, healAndShieldData);
        UpdateButtonTextColor(camouflageButtonText, currentCamouflageLevel, camouflageSkillData);
    }



    private void UpdateSkillCircle(List<GameObject> skillCircles, int currentLevel)
    {
        for (int i = 0; i < skillCircles.Count; i++)
        {
            skillCircles[i].SetActive(i == currentLevel);
        }
    }

    private void UpdateSkillDescriptionObjects(List<GameObject> skillDescriptions, int currentLevel)
    {
        for (int i = 0; i < skillDescriptions.Count; i++)
        {
            skillDescriptions[i].SetActive(i == currentLevel);
        }
    }
    private void UpdateAllSkillCircles()
    {
        UpdateSkillCircle(dashSkillCircles, currentDashLevel);
        UpdateSkillCircle(throwShurikenCircles, currentThrowShurikenLevel);
        UpdateSkillCircle(healAndShieldCircles, currentHealAndShieldLevel);
        UpdateSkillCircle(camouflageCircles, currentCamouflageLevel);
    }
    private void UpdateAllSkillDescriptions()
    {
        UpdateSkillDescriptionObjects(dashSkillDescriptions, currentDashLevel);
        UpdateSkillDescriptionObjects(throwShurikenDescription, currentThrowShurikenLevel);
        UpdateSkillDescriptionObjects(healAndShieldDescription, currentHealAndShieldLevel);
        UpdateSkillDescriptionObjects(camouflageDescription, currentCamouflageLevel);
    }

    public void SyncDataToDataManager()
    {
        DataManager.instance.currentDashLevel = currentDashLevel;
        DataManager.instance.currentThrowShurikenLevel = currentThrowShurikenLevel;
        DataManager.instance.currentHealAndShieldLevel = currentHealAndShieldLevel;
        DataManager.instance.currentCamouflageLevel = currentCamouflageLevel;

        DataManager.instance.currentDashData = currentDashData;
        DataManager.instance.currentThrowShurikenData = currentThrowShurikenData;
        DataManager.instance.currentHealAndShieldData = currentHealAndShieldData;
        DataManager.instance.currentCamouflageData = currentCamouflageData;
    }


}
