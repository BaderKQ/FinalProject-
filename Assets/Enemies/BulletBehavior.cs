using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    float moveSpeed = 10f;
    Rigidbody2D rb;

    ThePlayer target;
    Vector2 moveDirection; 

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>(); 
        target = GameObject.FindObjectOfType<ThePlayer>();
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy (gameObject, 3f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag=="Player"){
            Debug.Log("Hit");
        }
    }
}
