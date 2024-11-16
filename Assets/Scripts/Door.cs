using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Survivor")
        {
            UIManager.Instance.ChangeSurvivorCounter();
            PlayerStats.Instance.GiveMoney(5);
            Destroy(collision.gameObject);
        }
    }
}
