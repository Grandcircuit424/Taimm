using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IDamageable
{
    [SerializeReference]
    protected float MaxHealth;
    [SerializeReference]
    protected float Health;
    [SerializeReference]
    protected float Speed;
    [SerializeReference]
    protected float damage;
    [SerializeReference]
    protected float AggressionLevel;

    [Header("HealthBar")]
    [SerializeReference]
    protected Image HealthBar;
    [SerializeReference]
    protected Image HealthMeter;
    [SerializeReference]
    protected Transform Target;

    [SerializeField]
    public float SpawnWeight;
    [SerializeField]
    public float SpawnPoint;

    private void UpdateHealthBar()
    {
        HealthMeter.fillAmount = Health / MaxHealth;
        HealthMeter.color = Color.Lerp(Color.red, Color.green, Health / MaxHealth);
    }

    public virtual void Damage(float Damage)
    {
        Health -= Damage;
        UpdateHealthBar();
        if (Health <= 0) 
        {
            Destroy(gameObject);
            UIManager.Instance.ChangeKillCounter();
        }
    }

    private void Update()
    {
        HealthBar.transform.rotation = Camera.main.transform.rotation;
    }

    protected void CenterHealth()
    {
        HealthBar.transform.rotation = Camera.main.transform.rotation;
        HealthBar.transform.position = Target.transform.position;
    }
}
