using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    public Rigidbody2D rb;

    public float GroundSpeed = 1f;
    public bool CanJump = true;
    public float RaycastDistance = 1f;
    public float DistanceAdjuster = 1f;

    public bool LeftWallCling = false;
    public bool RightWallCling = false;

    bool leftInput = false;
    bool rightInput = false;
    bool jumpInput = false;
    bool fallInput = false;

    bool movingLeft = false;
    bool movingRight = false;

    public float JumpForce = 1f;
    public float WallJumpForce = 1f;
    public float ArtificialGravity = 1f;
    float currentGravity = 1f;

    bool CollisionCheck = false;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        currentGravity = ArtificialGravity;
    }

    void Update()
    {
        RegisterInputs();
        DetermineMovement();
        HorizontalMovement();
        if (jumpInput) Jumping();
        //if (fallInput) Falling();
        if (LeftWallCling || RightWallCling) ClimbingWalls();
    }

    void RegisterInputs()
    {
        leftInput = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        rightInput = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        jumpInput = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
        fallInput = Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S);
        //if (leftInput || rightInput) CollisionCheck = true;
    }

    void DetermineMovement()
    {
        movingLeft = leftInput && !rightInput;
        movingRight = rightInput && !leftInput;
    }

    void HorizontalMovement()
    {
        if (movingLeft && !WallDetection(Vector2.left, RaycastDistance) && !LeftWallCling && !RightWallCling)
        {
            if (CanJump) transform.position += new Vector3(-GroundSpeed * Time.deltaTime, 0);
            else transform.position += new Vector3((-GroundSpeed/2) * Time.deltaTime, 0, 0);

        }
        if (movingRight && !WallDetection(Vector2.right, RaycastDistance) && !LeftWallCling && !RightWallCling)
        {
            if (CanJump) transform.position += new Vector3(GroundSpeed * Time.deltaTime, 0, 0);
            else transform.position += new Vector3((GroundSpeed/2) * Time.deltaTime, 0, 0);
        }
    }

    bool WallDetection(Vector2 direction, float Distance)
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, direction, Distance, 255);

        if (hit) return true;
        else return false;
    }

    void Jumping()
    {
        if (CanJump)
        {
            currentGravity = ArtificialGravity;
            if (LeftWallCling) rb.AddForce(new Vector2(WallJumpForce, 0));
            if (RightWallCling) rb.AddForce(new Vector2(-WallJumpForce, 0));
            LeftWallCling = false;
            RightWallCling = false;
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2 (0, JumpForce));
            CanJump = false;
        }
    }

    void FixedUpdate()
    {
        //if (!CanJump)
        //{
        //    CanJump = WallDetection(Vector3.down);
        //}
        if (!LeftWallCling && !RightWallCling) rb.AddForce(new Vector2 (0, -ArtificialGravity));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        CheckCollision(collision);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        CheckCollision(collision);
        LeftWallCling = false;
        RightWallCling = false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (CollisionCheck == true)
        {
            CheckCollision(collision);
            CollisionCheck = false;
        }
    }

    void CheckCollision(Collision2D collision)
    {
        var direction = Vector2.zero;
        //print(direction);
        foreach (ContactPoint2D contact in collision.contacts)
        {
            direction += contact.point - new Vector2(transform.position.x, transform.position.y);
        }
        print(direction);


        if (direction.x < -0.8f && leftInput) LeftWallCling = true;
        if (direction.x > 0.8f && rightInput) RightWallCling = true;
        if (direction.y > 0.5f || direction.y < -0.5f)
        {
            LeftWallCling = false;
            RightWallCling = false;
        }

        if (!CanJump && collision.gameObject.CompareTag("Wall") && direction.y < 1)
        {
            CanJump = true;
        }
    }

    void ClimbingWalls()
    {
        currentGravity = 0;
        rb.velocity = Vector2.zero;
    }

    void Falling()
    {
        rb.AddForce(new Vector2 (0, -JumpForce * 2));
    }
}
