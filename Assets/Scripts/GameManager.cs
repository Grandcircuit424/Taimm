using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject EnemysFolder;

    [SerializeField]
    Playermovement playerMovementScript;

    [SerializeField]
    Shooting shootingScript;

    public static GameManager Instance;

    public int Round;

    public  GameState State;

    public static event Action<GameState> onGameStateChange;

    void Awake()
    {
        Instance = this;
        Round = 1;
    }

    private void Start()
    {
        UpdateGameState(GameState.SetUp);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.SetUp:
                playerMovementScript.enabled = true;
                shootingScript.enabled = true;
                break;
            case GameState.Intermission:
                playerMovementScript.enabled = false;
                shootingScript.enabled = false;
                break;
            case GameState.Wave:
                break;
            case GameState.End:
                StartCoroutine(WaitingForEnd());
                break;
            case GameState.Lose:
                Time.timeScale = 0f;
                playerMovementScript.enabled = false;
                shootingScript.enabled = false;
                break;
            case GameState.Win:
                break;
        }

        onGameStateChange?.Invoke(newState);
    }

    IEnumerator WaitingForEnd()
    {
        while (EnemysFolder.transform.childCount != 0)
        { 
            yield return new WaitForSeconds(1f);
        }
        
        
        if (Round == 10)
        {
            UpdateGameState(GameState.Win);
        } else
        {
            UpdateGameState(GameState.Intermission);
        }
        Round++;
    }

    public enum GameState
    {
        SetUp,
        Intermission,
        Wave,
        Lose,
        End,
        Win
    }
}
