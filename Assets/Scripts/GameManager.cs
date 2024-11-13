using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public  GameState State;

    public static event Action<GameState> onGameStateChange;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.SetUp);
    }

    public  void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.SetUp:
                break;
            case GameState.Wave:
                break;
            case GameState.End:
                StartCoroutine(WaitWave());
                break;
            case GameState.Lose:
                UIManager.Instance.GameOver();
                break;
        }

        onGameStateChange?.Invoke(newState);
    }

    IEnumerator WaitWave()
    {
        yield return new WaitForSeconds(20f);
        UpdateGameState(GameState.SetUp);
    }

    public enum GameState
    {
        SetUp,
        Wave,
        Lose,
        End
    }
}
