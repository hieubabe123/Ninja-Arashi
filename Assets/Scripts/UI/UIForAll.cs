using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIForAll : MonoBehaviour
{
    public static UIForAll instance;

    [Header("---------------Current Stat Displays---------------")]
    public TMP_Text currentLifeCountDisplay;
    public TMP_Text currentMoneyDisplay;
    public TMP_Text currentGemDisplay;
    public TMP_Text currentScrollPaperDisplay;

    [Header("---------------UI---------------")]
    public List<GameObject> disableObject = new List<GameObject>();
    public GameObject gameOverUI;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (DataManager.instance != null)
        {
            if (currentMoneyDisplay != null)
            {
                currentMoneyDisplay.text = DataManager.instance.CurrentMoney.ToString();
            }
            if (currentGemDisplay != null)
            {
                currentGemDisplay.text = DataManager.instance.CurrentGem.ToString();
            }
            if (currentScrollPaperDisplay != null)
            {
                currentScrollPaperDisplay.text = DataManager.instance.CurrentScrollPaper.ToString();
            }
        }
    }

    public void DisableObject()
    {
        foreach (var gameObj in disableObject)
        {
            if (gameObj != null)
            {
                gameObj.SetActive(false);
            }
        }
    }

    public void EnableGameOverUI()
    {
        gameOverUI.SetActive(true);
    }
}
