using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayer : MonoBehaviour
{
    public Rigidbody2D m_RigidBody;
    [SerializeField] float speed = 1.0f;
    public Vector3 Dir = Vector3.zero; //(0,0,0,)

    [SerializeField] public float jumpPower = 10f;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask WhatIsGround;
    private bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, WhatIsGround); //groundcheck
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {

            m_RigidBody.velocity = new Vector2(-speed, m_RigidBody.velocity.y);

        }

        if (Input.GetKey(KeyCode.D))//
        {
 
            m_RigidBody.velocity = new Vector2(speed, m_RigidBody.velocity.y);

        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            m_RigidBody.velocity = new Vector2(0, m_RigidBody.velocity.y);

        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            m_RigidBody.velocity = new Vector2(0, m_RigidBody.velocity.y);

        }


        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            m_RigidBody.velocity = Vector2.up * jumpPower;

        }

    }
}
