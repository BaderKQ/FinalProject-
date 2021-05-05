using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    float AttackTimer = 1f;
    float AttackTimeLeft = 1f;
    public GameObject Player;

    void Awake()
    {
        AttackTimer = Player.GetComponent<Player_Control>().AttackTimer;
        AttackTimeLeft = AttackTimer;
    }


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
