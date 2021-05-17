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
        if (collision.CompareTag("Enemy") || collision.CompareTag("Projectile") || collision.CompareTag("Patrolling Enemy") || collision.CompareTag("Hazard"))
        {
            Player.SendMessage("PlayerGetsHurt");
            if (collision.CompareTag("Hazard") && Player != null) Player.SendMessage("PlayerGetsHurt");
            if (Player.transform.position.x < collision.gameObject.transform.position.x) rb.AddForce(new Vector2(-Knockback, Knockback));
            else if (Player.transform.position.x > collision.gameObject.transform.position.x) rb.AddForce(new Vector2(Knockback, Knockback));
            else rb.AddForce(new Vector2(0, Knockback));
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Enemy") || collision2D.gameObject.CompareTag("Projectile")|| collision2D.gameObject.CompareTag("Patrolling Enemy") || collision2D.gameObject.CompareTag("Hazard"))
        {
            Player.SendMessage("PlayerGetsHurt");
            if (collision2D.gameObject.CompareTag("Hazard")) Player.SendMessage("PlayerGetsHurt");
            if (Player.transform.position.x < collision2D.gameObject.transform.position.x) rb.AddForce(new Vector2(-Knockback, Knockback));
            else if (Player.transform.position.x > collision2D.gameObject.transform.position.x) rb.AddForce(new Vector2(Knockback, Knockback));
            else rb.AddForce(new Vector2(0, Knockback));
        }
    }
}
