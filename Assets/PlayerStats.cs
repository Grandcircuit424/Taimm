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

    [SerializeField]
    public float ShockWave;
    [SerializeField]
    public float Medkits;

    private void Awake()
    {
        Instance = this;
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

    public void GiveMoney(float GiveMoney)
    {
        Money += GiveMoney;
    }

    public void SpentShockwave()
    {
        ShockWave -= 1;
    }

    public void SpentMedkits()
    {
        Medkits -= 1;
    }


    public void BuyShockwave()
    {
        ShockWave += 1;
    }

    public void BuyMedkit()
    {
        Medkits += 1;
    }

    public void Heal()
    {
        health = Maxhealth;
        UIManager.Instance.ChangeHealthBar(health);
    }

    public void Heal(float HealthGained)
    {
        health = Mathf.Clamp(health + HealthGained, 0 ,Maxhealth);
        UIManager.Instance.ChangeHealthBar(health);
    }

    public void IncreaseHealthAmmount()
    {
        Maxhealth += 25;
        Heal();
    }

    public void IncreaseAmmoDamager()
    {
        BulletDamage += 2;
    }
}
