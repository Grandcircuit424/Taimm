using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Airplane : MonoBehaviour
{
    public static Airplane Instance;

    [SerializeField]
    public float Health;

    void Awake()
    {
        Instance = this;
    }

    public void DamagePlane(float Damage)
    {
        Health -= Damage;
        if (Health <= 0)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Lose);
        }
    }
}
