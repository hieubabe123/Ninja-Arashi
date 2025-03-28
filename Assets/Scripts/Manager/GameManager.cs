using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum GameState
    {
        GamePlay, Paused, GameOver
    }

    public GameState currentState;
    public GameState previousState;

    public bool isGameOver = false;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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

    #region State
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
                    EndGame();
                }
                break;
            default:
                Debug.LogWarning("State not exist");
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
            //pauseScreen.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            currentState = previousState;
            currentState = GameState.GamePlay;
            Time.timeScale = 1.0f;
            //pauseScreen.SetActive(false);
        }
    }

    private void EndGame()
    {
        isGameOver = true;
        Time.timeScale = 0f;
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
    #endregion

}
