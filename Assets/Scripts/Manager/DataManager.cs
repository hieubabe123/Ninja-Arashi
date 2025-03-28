using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;


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
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            LoadSkillData();
            UpgradeCurrentSkillData();
        }
        else
        {
            Destroy(gameObject);
        }
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


    public void SaveSkillData()
    {
        PlayerPrefs.SetInt("DashLevel", currentDashLevel);
        PlayerPrefs.SetInt("ThrowShurikenLevel", currentThrowShurikenLevel);
        PlayerPrefs.SetInt("HealShieldLevel", currentHealAndShieldLevel);
        PlayerPrefs.SetInt("CamouflageLevel", currentCamouflageLevel);
        PlayerPrefs.Save();
    }

    public void LoadSkillData()
    {
        currentDashLevel = PlayerPrefs.GetInt("DashLevel", 0);
        currentThrowShurikenLevel = PlayerPrefs.GetInt("ThrowShurikenLevel", 0);
        currentHealAndShieldLevel = PlayerPrefs.GetInt("HealShieldLevel", 0);
        currentCamouflageLevel = PlayerPrefs.GetInt("CamouflageLevel", 0);
    }
}
