using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    public Rigidbody rb;

    public float GroundSpeed = 1f;
    public bool CanJump = true;
    public float RaycastDistance = 1f;

    public bool WallCling = false;

    bool leftInput = false;
    bool rightInput = false;
    bool jumpInput = false;
    bool fallInput = false;

    bool movingLeft = false;
    bool movingRight = false;

    public float JumpForce = 1f;
    public float ArtificialGravity = 1f;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        RegisterInputs();
        DetermineMovement();
        HorizontalMovement();
        if (jumpInput) Jumping();
        //if (fallInput) Falling();
        if (WallCling) ClimbingWalls();
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
        if (movingLeft && !WallDetection(Vector3.left) && !WallCling)
        {
            if (CanJump) transform.position += new Vector3(-GroundSpeed * Time.deltaTime, 0, 0);
            else transform.position += new Vector3((-GroundSpeed/2) * Time.deltaTime, 0, 0);
        }
        if (movingRight && !WallDetection(Vector3.right) && !WallCling)
        {
            if (CanJump) transform.position += new Vector3(GroundSpeed * Time.deltaTime, 0, 0);
            else transform.position += new Vector3((GroundSpeed/2) * Time.deltaTime, 0, 0);
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
            rb.useGravity = true;
            WallCling = false;
            rb.velocity = Vector3.zero;
            rb.AddForce(0, JumpForce, 0);
            CanJump = false;
        }
    }

    void FixedUpdate()
    {
        //if (!CanJump)
        //{
        //    CanJump = WallDetection(Vector3.down);
        //}
        if (!WallCling) rb.AddForce(0, -ArtificialGravity, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!CanJump && collision.gameObject.CompareTag("Wall"))
        {
            CanJump = true;
        }
        if (WallDetection(Vector3.left) || WallDetection(Vector3.right))
        {
            WallCling = true;
        }
    }

    void ClimbingWalls()
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
    }

    void Falling()
    {
        rb.AddForce(0, -JumpForce * 2, 0);
    }
}
