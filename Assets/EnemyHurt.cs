using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHurt : MonoBehaviour
{
    public float MaxHP = 1;
    public float CurrentHP = 1;

    void Start()
    {
        CurrentHP = MaxHP;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        print("Blah");
        if (collision.CompareTag("PlayerStrike"))
        {
            print("THE Blah");
            CurrentHP -= 1;
            if (CurrentHP <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
