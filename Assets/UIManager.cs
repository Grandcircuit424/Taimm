using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    TextMeshProUGUI WaveCounter;
    [SerializeField]
    TextMeshProUGUI SurvivorCounter;
    [SerializeField]
    TextMeshProUGUI KillCounter;
    [SerializeField]
    TextMeshProUGUI GameOverScreen;
    [SerializeField]
    Image HealthBar;
    [SerializeField]
    Image AirplanesHealthBar;

    [SerializeField]
    GameObject ShopMenu;

    [SerializeField]
    int SurvivorCount = 0;

    [SerializeField]
    int KillCount = 0;

    void Awake()
    {
        Instance = this;
        ChangeHealthBar(PlayerStats.Instance.health);
    }

    public void ChangeWaveCounter(int Wave)
    {
        WaveCounter.text = "Day " + Wave;
    }

    public void ChangeSurvivorCounter()
    {
        SurvivorCount++;
        SurvivorCounter.text = "Survivors Saved: " + SurvivorCount;
    }

    public void ChangeKillCounter()
    {
        KillCount++;
        KillCounter.text = "Zombies Killed: " + KillCount;
    }

    public void GameOver()
    {
        GameOverScreen.enabled = true;
    }

    public void ChangeHealthBar(float Health)
    {
        HealthBar.fillAmount = Health / 50f;
    }

    public void ChangeAirplanesHealthBar(float CurrentHealth, float MaxHealth)
    {
        AirplanesHealthBar.fillAmount = CurrentHealth / MaxHealth;
        AirplanesHealthBar.color = Color.Lerp(Color.red, Color.green, CurrentHealth / MaxHealth);
    }

    public void ShopMenuOn()
    {
        ShopMenu.SetActive(true);
    }

    public void ShopMenuOff() 
    {
        ShopMenu.SetActive(false);
    }

    public void EndOfShop()
    {
        ShopMenuOff();
        GameManager.Instance.UpdateGameState(GameManager.GameState.SetUp);
    }
}
