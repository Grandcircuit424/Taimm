using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject EnemysFolder;

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
            case GameState.Intermission:
                break;
            case GameState.Wave:
                break;
            case GameState.End:
                StartCoroutine(WaitingForEnd());
                break;
            case GameState.Lose:
                UIManager.Instance.GameOver();
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
        UpdateGameState(GameState.Intermission);
    }

    public enum GameState
    {
        SetUp,
        Intermission,
        Wave,
        Lose,
        End
    }
}
