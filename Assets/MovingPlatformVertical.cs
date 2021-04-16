using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformVertical : MonoBehaviour
{
    private Rigidbody2D m_RigidBody;
    [SerializeField] private float Speed = 1.0f;
    [SerializeField] private Vector3 M_Dir = Vector3.zero;

    [SerializeField] float topY;
    [SerializeField] float bottomY;
    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 velocity;
        velocity = Speed * M_Dir;

        m_RigidBody.velocity = velocity;

        if (transform.position.y >= topY)
        {
            M_Dir = Vector3.down;

        }

        if (transform.position.y <= bottomY)
        {
            M_Dir = Vector3.up;

        }
    }

}
