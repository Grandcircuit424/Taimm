using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float Damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null )
        {
            damageable.Damage(Damage);
        }
    }

    void Start()
    {
        Destroy(gameObject, 5f);
    }
}
