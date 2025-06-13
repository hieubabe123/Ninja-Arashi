using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void SceneChange(string sceneName)
    {
        StartCoroutine(LoadSceneAndData(sceneName));
    }

    private IEnumerator LoadSceneAndData(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
        yield return null; // Wait for the scene to load

        SaveAndLoadManager.LoadGame();
    }

    public void OnUpgradeButtonPress(string sceneName)
    {
        if (DataManager.instance != null)
        {
            DataManager.instance.activePanelOnLoad = DataManager.TargetPanel.Upgrade;
        }
        SceneChange(sceneName);
    }

    public void OnShopButtonPress(string sceneName)
    {
        if (DataManager.instance != null)
        {
            DataManager.instance.activePanelOnLoad = DataManager.TargetPanel.Shop;
        }
        SceneChange(sceneName);
    }

    public void SceneRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
