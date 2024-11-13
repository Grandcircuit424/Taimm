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
    int SurvivorCount = 0;

    [SerializeField]
    int KillCount = 0;

    void Awake()
    {
        Instance = this;
    }

    public void ChangeWaveCounter(int Wave)
    {
        WaveCounter.text = "Wave: " + Wave;
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
}
