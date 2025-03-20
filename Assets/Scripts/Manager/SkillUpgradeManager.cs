using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillUpgradeManager : MonoBehaviour
{
    public static SkillUpgradeManager instance;

    [Header("---------------Skill Upgrade Data---------------")]
    public List<DashSkillScriptableObject> dashSkillData = new List<DashSkillScriptableObject>();
    public List<ThrowShurikenScriptableObject> throwShurikenData = new List<ThrowShurikenScriptableObject>();
    public List<HealAndShieldScriptableObject> healAndShieldData = new List<HealAndShieldScriptableObject>();
    public List<CamouflageScriptableObject> camouflageSkillData = new List<CamouflageScriptableObject>();

    [Header("---------------Current Skill Data---------------")]
    public DashSkillScriptableObject currentDashData;
    public ThrowShurikenScriptableObject currentThrowShurikenData;
    public HealAndShieldScriptableObject currenthealAndShieldData;
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


    [Header("---------------Player Data---------------")]
    public PlayerScriptableObject playerData;

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
        LoadSkillData();
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        UpdateAllSkillCircles();
        UpdateAllSkillDescriptions();
        UpgradeCurrentSkillData();
        UpdateSkillButtonColors();
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
        UpgradeCurrentSkillData();
        UpdateAllSkillDescriptions();
        SaveSkillData();
    }

    private void UpdateButtonColor<T>(TMP_Text buttonText, int currentLevel, List<T> skillDataList) where T : SkillUpgradeScriptableObject
    {
        if (currentLevel < skillDataList.Count - 1)
        {
            T nextSkill = skillDataList[currentLevel + 1];
            if (playerData.Money >= nextSkill.MoneyToUpgrade && playerData.Gem >= nextSkill.GemToUpgrade)
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
        if (playerData.Money >= nextSkill.MoneyToUpgrade && playerData.Gem >= nextSkill.GemToUpgrade)
        {

            playerData.Money -= nextSkill.MoneyToUpgrade;
            playerData.Gem -= nextSkill.GemToUpgrade;
            GameManager.instance.currentMoneyDisplay.text = playerData.Money.ToString();
            GameManager.instance.currentGemDisplay.text = playerData.Gem.ToString();

            currentLevel++;
            UpdateSkillCircle(skillCircles, currentLevel);
        }
        else
        {
            return;
        }
        UpdateSkillButtonColors();

    }

    private void UpdateSkillButtonColors()
    {
        UpdateButtonColor(dashSkillButtonText, currentDashLevel, dashSkillData);
        UpdateButtonColor(throwShurikenButtonText, currentThrowShurikenLevel, throwShurikenData);
        UpdateButtonColor(healAndShieldButtonText, currentHealAndShieldLevel, healAndShieldData);
        UpdateButtonColor(camouflageButtonText, currentCamouflageLevel, camouflageSkillData);
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

    private void UpgradeCurrentSkillData()
    {
        if (dashSkillData.Count > 0)
        {
            currentDashData = dashSkillData[Mathf.Clamp(currentDashLevel, 0, dashSkillData.Count - 1)];
        }
        if (throwShurikenData.Count > 0)
        {
            currentThrowShurikenData = throwShurikenData[Mathf.Clamp(currentThrowShurikenLevel, 0, throwShurikenData.Count - 1)];
        }
        if (healAndShieldData.Count > 0)
        {
            currenthealAndShieldData = healAndShieldData[Mathf.Clamp(currentHealAndShieldLevel, 0, healAndShieldData.Count - 1)];
        }
        if (camouflageSkillData.Count > 0)
        {
            currentCamouflageData = camouflageSkillData[Mathf.Clamp(currentCamouflageLevel, 0, camouflageSkillData.Count - 1)];
        }
    }

    private void SaveSkillData()
    {
        PlayerPrefs.SetInt("DashLevel", currentDashLevel);
        PlayerPrefs.SetInt("ThrowShurikenLevel", currentThrowShurikenLevel);
        PlayerPrefs.SetInt("HealShieldLevel", currentHealAndShieldLevel);
        PlayerPrefs.SetInt("CamouflageLevel", currentCamouflageLevel);
        PlayerPrefs.Save();
    }

    private void LoadSkillData()
    {
        currentDashLevel = PlayerPrefs.GetInt("DashLevel", 0);
        currentThrowShurikenLevel = PlayerPrefs.GetInt("ThrowShurikenLevel", 0);
        currentHealAndShieldLevel = PlayerPrefs.GetInt("HealShieldLevel", 0);
        currentCamouflageLevel = PlayerPrefs.GetInt("CamouflageLevel", 0);
    }
}
