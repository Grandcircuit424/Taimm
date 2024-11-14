using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    public static PlayerStats Instance;

    [SerializeField]
    public float Maxhealth;
    [SerializeField]
    public float health;
    [SerializeField]
    public float Money;
    [SerializeField]
    public float BulletDamage;

    private void Awake()
    {
        Instance = this;
        Money = 100;
        BulletDamage = 3;
    }

    public void Damage(float Damage)
    {
        health -= Damage;
        UIManager.Instance.ChangeHealthBar(health);
        if (health <= 0)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Lose);
        }
    }

    public void SpendMoney(float Amountspent)
    {
        Money -= Amountspent;
    }

    public void Heal()
    {
        health = Maxhealth;
        UIManager.Instance.ChangeHealthBar(health);
    }

    public void IncreaseHealthAmmount()
    {
        Maxhealth += 25;
        Heal();
    }

    public void IncreaseAmmoDamager()
    {
        BulletDamage += 3;
    }
}
