using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField]
    float health;

    public void Damage(float Damage)
    {
        health -= Damage;
        UIManager.Instance.ChangeHealthBar(health);
        if (health <= 0)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Lose);
        }
    }
}
