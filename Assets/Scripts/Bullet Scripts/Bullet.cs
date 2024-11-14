using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null && collision.gameObject.tag != "Attackable")
        {
            damageable.Damage(PlayerStats.Instance.BulletDamage);
        }
    }

    void Start()
    {
        Destroy(gameObject, 5f);
    }
}
