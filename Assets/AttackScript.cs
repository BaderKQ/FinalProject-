using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public float AttackTimer = 1f;
    float AttackTimeLeft = 1f;
    public GameObject Player;


    void Update()
    {
        AttackTimeLeft -= Time.deltaTime;
        if (AttackTimeLeft <= 0)
        {
            AttackTimeLeft = AttackTimer;
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Player.SendMessage("ResetJump");
        }
    }
}
