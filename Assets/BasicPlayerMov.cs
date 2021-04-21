using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerMov : MonoBehaviour
{
    public float JumpForce = 1f;
    public float GroundSpeed = 1f;
    public bool CanJump = true;
    public float RaycastDistance = 1f;

    public Rigidbody2D rb;

    bool leftInput = false;
    bool rightInput = false;
    bool jumpInput = false;
    bool fallInput = false;

    bool movingLeft = false;
    bool movingRight = false;

    

    void Start()
    {
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        RegisterInputs();
        DetermineMovement();
        HorizontalMovement();
        if (jumpInput) Jumping();
    }

    void RegisterInputs()
    {
        leftInput = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        rightInput = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        jumpInput = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
        fallInput = Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S);
    }

    void DetermineMovement()
    {
        movingLeft = leftInput && !rightInput;
        movingRight = rightInput && !leftInput;
    }

    void HorizontalMovement()
    {
        if (movingLeft && !WallDetection(Vector3.left))
        {
            if (CanJump) transform.position += new Vector3(-GroundSpeed * Time.deltaTime, 0, 0);
            else transform.position += new Vector3((-GroundSpeed / 2) * Time.deltaTime, 0, 0);
        }
        if (movingRight && !WallDetection(Vector3.right))
        {
            if (CanJump) transform.position += new Vector3(GroundSpeed * Time.deltaTime, 0, 0);
            else transform.position += new Vector3((GroundSpeed / 2) * Time.deltaTime, 0, 0);
        }
    }

    bool WallDetection(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, RaycastDistance, 255)) return true;
        else return false;
    }

    void Jumping()
    {
        if (CanJump)
        {
            rb.AddForce(new Vector2(0, JumpForce));
            CanJump = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!CanJump && collision.gameObject.CompareTag("Wall"))
        {
            CanJump = true;
        }
    }
}
