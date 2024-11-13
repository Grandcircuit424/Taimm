using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : Enemy, IDamageable
{
    GameObject EnemysTarget;

    [SerializeField]
    Rigidbody2D RB;

    bool Attacked = false;

    [SerializeField]
    Collider2D Collider;

    private void Start()
    {
        InvokeRepeating("ShouldTargetChange", 5f, 5f);
    }

    private void Update()
    {
        RB.velocity = Vector3.zero;
        RB.angularVelocity = 0f;

        HealthBar.transform.rotation = Camera.main.transform.rotation;
        HealthBar.transform.position = Target.transform.position;

        if (EnemysTarget == null)
        {
            StartCoroutine(FindTarget());
        }
        else if (EnemysTarget != null && !Attacked)
        {
            Collider2D[] Targets = Physics2D.OverlapCircleAll(gameObject.transform.position, .95f);
            StartCoroutine(Attacking(Targets));
        }
    }
    private void FixedUpdate()
    {   
        if (EnemysTarget)
        {
            Vector2 lookdir = EnemysTarget.transform.position - transform.position;

            float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f;
            RB.rotation = Mathf.LerpAngle(RB.rotation, angle, .27f);

            RB.position = Vector2.MoveTowards(transform.position, EnemysTarget.transform.position, Speed * Time.fixedDeltaTime);
        }
    }

    IEnumerator Attacking(Collider2D[] Targets)
    {
        if (Targets == null) yield return null; 
        foreach (Collider2D target in Targets)
        {
            if (target == EnemysTarget.GetComponent<Collider2D>())
            {
                IDamageable damageable = target.gameObject.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    Attacked = true;
                    damageable.Damage(damage);
                    yield return new WaitForSeconds(1f);
                    Attacked = false;
                }
                break;
            }

        }
    } 

    private void OnDrawGizmos()
    {
        if (EnemysTarget != null) { 
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(gameObject.transform.position, EnemysTarget.transform.position);
        }
    }

    IEnumerator FindTarget()
    {
        Collider2D[] Targets = Physics2D.OverlapCircleAll(gameObject.transform.position, 100);

        float CloseDistance = 100;

        foreach (Collider2D target in Targets)
        {
            Collider2D TargetsCollider = target.GetComponent<Collider2D>();

            if (target.tag == "Survivor" || target.tag == "Attackable" || target.tag == "Player")
            {
                float distance = Vector2.Distance(gameObject.transform.position, target.transform.position);
                if (distance < CloseDistance) {
                    CloseDistance = distance;
                    EnemysTarget = target.gameObject;
                }
            }
        }
        yield return new WaitForSeconds(.2f);
    }

    void ShouldTargetChange()
    {
        float ChangeChance = AggressionLevel / 100;

        float RanChance = Random.Range(0.0f, 1.0f);
        if (ChangeChance < RanChance) 
        {
            StartCoroutine(FindTarget());
        }
    }
}
