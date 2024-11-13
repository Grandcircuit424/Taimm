using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    [SerializeField]
    float Speed;

    [SerializeField]
    Rigidbody2D RB;

    [SerializeField]
    Camera Camera;

    [SerializeField]
    CinemachineVirtualCamera Cinemachine;

    Vector2 MovementDirection;
    Vector2 mousePos;

    // Update is called once per frame
    void Update()
    {
        MovementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        mousePos = Camera.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        RB.MovePosition(RB.position + MovementDirection * Speed * Time.fixedDeltaTime);

        Vector2 lookdir = mousePos - RB.position;
        float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg -90f;

        RB.rotation = angle;


    }
}
