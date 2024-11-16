using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    TextMeshProUGUI ShockWave;
    [SerializeField]
    TextMeshProUGUI Medkits;

    [SerializeField]
    GameObject ShopMenu;

    [SerializeField]
    Image SWCooldownGO;

    [SerializeField]
    int SurvivorCount = 0;

    [SerializeField]
    int KillCount = 0;

    void Awake()
    {
        Instance = this;
        ChangeHealthBar(PlayerStats.Instance.health);
        GameManager.onGameStateChange += GameManagerOnStateChange;
        UpdateEquipment();
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChange -= GameManagerOnStateChange;
    }

    private void GameManagerOnStateChange(GameManager.GameState state)
    {
        switch (state) 
        {
            case (GameManager.GameState.SetUp):
                ChangeWaveCounter(GameManager.Instance.Round);
                ChangeAirplanesHealthBar(Airplane.Instance.Health, Airplane.Instance.MaxHealth);
                break;
            case (GameManager.GameState.Intermission):
                ShopMenuOn();
                break;
            case (GameManager.GameState.Lose):
                GameOver();
                break;
        }
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
        GameOverScreen.gameObject.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void ChangeHealthBar(float Health)
    {
        HealthBar.fillAmount = Health / PlayerStats.Instance.Maxhealth;
    }

    public void ChangeAirplanesHealthBar(float CurrentHealth, float MaxHealth)
    {
        AirplanesHealthBar.fillAmount = CurrentHealth / MaxHealth;
        AirplanesHealthBar.color = Color.Lerp(Color.red, Color.green, CurrentHealth / MaxHealth);
    }

    public void ShopMenuOn()
    {
        ShopMenu.SetActive(true);
        ShopManager.Instance.RepairText();
        ShopManager.Instance.CurrencyUpdate();
    }

    public void UpdateEquipment()
    {
        ShockWave.text = PlayerStats.Instance.ShockWave.ToString();
        Medkits.text = PlayerStats.Instance.Medkits.ToString();
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

    public void ChangeCooldownTimer(float Current, float OutOf)
    {
        SWCooldownGO.fillAmount = Current / OutOf;
    }
}
