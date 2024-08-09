using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Playing,
    Pause,
    GameOver
}

public class SceneManager : MonoBehaviour
{
    [HideInInspector]
    public static SceneManager Instance { get; private set; }
     public GameState CurrentGameState { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start(){
        ChangeGameState(GameState.Playing);
    }

    public void ChangeGameState(GameState newGameState)
    {
        CurrentGameState = newGameState;
        if(newGameState == GameState.MainMenu)
        {
            // Open main menu
        }
        else if(newGameState == GameState.Playing)
        {
            Time.timeScale = 1f;
        }
        else if(newGameState == GameState.Pause)
        {
            Time.timeScale = 0;
        }
        else if(newGameState == GameState.GameOver)
        {
            // Game over
        }
        Debug.Log($"Game state changed to: {CurrentGameState}");
    }
}
