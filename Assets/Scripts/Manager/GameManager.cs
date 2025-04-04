using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum GameState
    {
        GamePlay, Paused, GameOver, GameWin
    }

    public GameState currentState;
    public GameState previousState;

    public bool isGameOver = false;
    public bool isGameWin = false;

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

    private void Update()
    {
        ShowCurrentState();
    }

    private void ShowCurrentState()
    {
        switch (currentState)
        {
            case GameState.GamePlay:
                break;
            case GameState.Paused:
                break;
            case GameState.GameOver:
                if (!isGameOver)
                {
                    EndGameOver();
                }
                break;
            case GameState.GameWin:
                if (!isGameWin)
                {
                    EndGameWin();
                }
                break;
            default:
                break;
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

    public void PauseGame()
    {
        if (currentState != GameState.Paused)
        {
            previousState = currentState;
            currentState = GameState.Paused;
            Time.timeScale = 0f;
        }
    }

    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            currentState = previousState;
            currentState = GameState.GamePlay;
            Time.timeScale = 1.0f;
        }
    }

    private void EndGameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        UIForAll.instance.EnableGameOverUI();
        UIForAll.instance.DisableObject();
    }

    private void EndGameWin()
    {
        isGameWin = true;
        SaveAndLoadManager.SaveGame();
        UIForAll.instance.DisableObject();
        FindObjectOfType<SceneController>().SceneChange("ChooseMap");
    }

    public void OnClickPauseButton()
    {
        if (currentState == GameState.GamePlay)
        {
            PauseGame();
        }
    }

    public void OnClickResumeButton()
    {
        if (currentState == GameState.Paused)
        {
            ResumeGame();
        }
    }

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }

    public void GameWin()
    {
        StartCoroutine(WaitToEndGameWin());
    }

    private IEnumerator WaitToEndGameWin()
    {
        yield return new WaitForSeconds(2);
        ChangeState(GameState.GameWin);
    }

}
