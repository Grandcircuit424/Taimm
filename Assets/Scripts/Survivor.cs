using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survivor : NPC, IDamageable
{

    [SerializeField]
    GameObject door;

    [SerializeField]
    Rigidbody2D RB;

    enum State
    {
        Running,
        Scared
    }

    void Start()
    {
        door = GameObject.Find("Door");
    }
    void Update()
    {
        RB.velocity = Vector3.zero;
        

        Vector2 lookdir = door.transform.position - transform.position;

        float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f;
        RB.rotation = angle;
    }

    private void FixedUpdate()
    {
        RB.position = Vector2.MoveTowards(transform.position, door.transform.position, Speed * Time.fixedDeltaTime);
    }
}
