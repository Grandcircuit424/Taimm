using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour, IDamageable
{
    public void Damage(float Damage)
    {
        Airplane.Instance.DamagePlane(Damage);
    }
}
