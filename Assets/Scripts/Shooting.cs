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

    [SerializeField]
    AudioSource Source;

    [SerializeField]
    AudioClip Gunsound;

    [SerializeField]
    AudioClip ShockwaveAudio;

    [SerializeField]
    GameObject ShockwaveGO;

    [SerializeField]
    bool SWCooldownBool = false;

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
        if (Input.GetKeyDown(KeyCode.E) && PlayerStats.Instance.ShockWave > 0 && !SWCooldownBool)
        {
            StartCoroutine(Cooldown());
            Shockwave();
            PlayerStats.Instance.SpentShockwave();
            UIManager.Instance.UpdateEquipment();
        } 
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firepoint.up * Force, ForceMode2D.Impulse);
        Source.PlayOneShot(Gunsound);
    }

    void Shockwave()
    {
        GameObject Wave = Instantiate(ShockwaveGO, gameObject.transform);
        Wave.transform.parent = null;
        StartCoroutine(WaveAnimation(Wave));
        Collider2D[] Targets = Physics2D.OverlapCircleAll(gameObject.transform.position, 3f);
        foreach (Collider2D target in Targets)
        {
            IDamageable damageable = target.GetComponent<IDamageable>();
            if (damageable != null && target.tag != "Player" && target.tag != "Attackable")
            {
                damageable.Damage(7);
            }
        }
    }

    IEnumerator WaveAnimation(GameObject Wave)
    {
        Source.PlayOneShot(ShockwaveAudio);
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSecondsRealtime(.05f); 
            Wave.transform.localScale = new Vector3(Wave.transform.localScale.x + 1f - (i*.1f), Wave.transform.localScale.y + 1 - (i * .1f), Wave.transform.localScale.z);
        }
        yield return new WaitForSecondsRealtime(.05f);
        Destroy(Wave);


    }

    IEnumerator Cooldown()
    {
        SWCooldownBool = true;
        for (int i = 15; i >= 0;i--)
        {
            UIManager.Instance.ChangeCooldownTimer(i,15);
            yield return new WaitForSeconds(1f);
        }
        SWCooldownBool = false;
    }
}

