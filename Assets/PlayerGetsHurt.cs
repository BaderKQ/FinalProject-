using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetsHurt : MonoBehaviour
{
    public GameObject Player;
    private Rigidbody2D rb;
    public float Knockback = 1f;

    public float MaxInvTimer = 2f;
    public float InvTimeLeft = 0f;

    void Start()
    {
        rb = Player.GetComponent<Rigidbody2D>();
        InvTimeLeft = 0;
    }

    void Update()
    {
        if (InvTimeLeft > 0) InvTimeLeft -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (InvTimeLeft <= 0)
        {
            if (collision.CompareTag("Enemy") || collision.CompareTag("Projectile") || collision.CompareTag("Patrolling Enemy") || collision.CompareTag("Hazard"))
            {
                if (collision.CompareTag("Hazard")) Player.SendMessage("PlayerGetsHurt", 2);
                else Player.SendMessage("PlayerGetsHurt", 1);
                InvTimeLeft = MaxInvTimer;
                if (collision.CompareTag("Hazard") && Player != null) Player.SendMessage("PlayerGetsHurt");
                if (Player.transform.position.x < collision.gameObject.transform.position.x) rb.AddForce(new Vector2(-Knockback, Knockback));
                else if (Player.transform.position.x > collision.gameObject.transform.position.x) rb.AddForce(new Vector2(Knockback, Knockback));
                else rb.AddForce(new Vector2(0, Knockback));
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (InvTimeLeft <= 0)
        {
            if (collision2D.gameObject.CompareTag("Enemy") || collision2D.gameObject.CompareTag("Projectile") || collision2D.gameObject.CompareTag("Patrolling Enemy") || collision2D.gameObject.CompareTag("Hazard"))
            {
                if (collision2D.gameObject.CompareTag("Hazard")) Player.SendMessage("PlayerGetsHurt", 2);
                else Player.SendMessage("PlayerGetsHurt", 1);
                InvTimeLeft = MaxInvTimer;
                if (collision2D.gameObject.CompareTag("Hazard")) Player.SendMessage("PlayerGetsHurt");
                if (Player.transform.position.x < collision2D.gameObject.transform.position.x) rb.AddForce(new Vector2(-Knockback, Knockback));
                else if (Player.transform.position.x > collision2D.gameObject.transform.position.x) rb.AddForce(new Vector2(Knockback, Knockback));
                else rb.AddForce(new Vector2(0, Knockback));
            }
        }
    }
}
