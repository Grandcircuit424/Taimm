using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Airplane : MonoBehaviour
{
    public static Airplane Instance;

    [SerializeField]
    public float MaxHealth;

    [SerializeField]
    public float Health;

    void Awake()
    {
        Instance = this;
        GameManager.onGameStateChange += GameManagerOnStateChange;
    }

    private void GameManagerOnStateChange(GameManager.GameState state)
    {
        if (state == GameManager.GameState.SetUp) HealPlane();
    }

    public void DamagePlane(float Damage)
    {
        Health -= Damage;
        UIManager.Instance.ChangeAirplanesHealthBar(Health, MaxHealth);
        if (Health <= 0)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Lose);
        }
    }

    public void HealPlane()
    {
        Health = MaxHealth;
    }
}
