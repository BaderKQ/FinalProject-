using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetsHurt : MonoBehaviour
{
    public GameObject Player;
    private Rigidbody2D rb;
    public float Knockback = 1f;

    void Start()
    {
        rb = Player.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Projectile"))
        {
            Player.SendMessage("PlayerGetsHurt");
            if (Player.transform.position.x < collision.gameObject.transform.position.x) rb.AddForce(new Vector2(-Knockback, Knockback));
            else if (Player.transform.position.x > collision.gameObject.transform.position.x) rb.AddForce(new Vector2(Knockback, Knockback));
            else rb.AddForce(new Vector2(0, Knockback));
        }
    }
}
