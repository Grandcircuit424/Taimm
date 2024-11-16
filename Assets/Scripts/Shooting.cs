using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    public Transform firepoint;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    float Force;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Q) && PlayerStats.Instance.Medkits > 0)
        {
            PlayerStats.Instance.Heal(15f);
            PlayerStats.Instance.SpentMedkits();
            UIManager.Instance.UpdateEquipment();
        }
        if (Input.GetKeyDown(KeyCode.E) && PlayerStats.Instance.ShockWave > 0)
        {
            Shockwave();
            PlayerStats.Instance.SpentShockwave();
            UIManager.Instance.UpdateEquipment();
        } 
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firepoint.up * Force, ForceMode2D.Impulse);
    }

    void Shockwave()
    {
        Collider2D[] Targets = Physics2D.OverlapCircleAll(gameObject.transform.position, 3f);
        foreach (Collider2D target in Targets)
        {
            IDamageable damageable = target.GetComponent<IDamageable>();
            if (damageable != null && target.tag != "Player" && target.tag != "Airplane")
            {
                damageable.Damage(7);
            }
        }
    }
}

