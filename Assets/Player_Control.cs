using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player_Control : MonoBehaviour
{
    public Rigidbody2D rb;

    bool facingRight = true;

    public int MaxPlayerHealth = 5;
    public int PlayerHealthLeft = 5;

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
    public float MaxJumpTime = 0.01f;
    public float JumpTimer = 0.01f;
    public float WallJumpForce = 1f;
    public float ArtificialGravity = 1f;
    float currentGravity = 1f;

    public GameObject RightAttack;
    public GameObject LeftAttack;
    public float AttackTimer = 1f;
    float AttackTimeLeft = 1f;
    public bool CanAttack = true;

    bool CollisionCheck = false;

    //Animations
    public int SetAnimaion = 0;
    public GameObject StandRight; //1
    public GameObject WalkRight; //2
    public GameObject JumpRight; //3
    public GameObject ClingRight; //4
    public GameObject StandLeft; //5
    public GameObject WalkLeft; //6
    public GameObject JumpLeft; //7
    public GameObject ClingLeft; //8


    public GameObject AttackRight; //9
    public GameObject JumpAttackRight; //10
    public GameObject AttackLeft; //11
    public GameObject JumpAttackLeft; //12

    bool isMoving = false;
    bool isJumping = false;


    void Start()
    {

        rb = gameObject.GetComponent<Rigidbody2D>();
        currentGravity = ArtificialGravity;
        facingRight = true;
        JumpTimer = 0;
        PlayerHealthLeft = MaxPlayerHealth;
        UpdateDisplay();

        RightAttack.SetActive(false);
        LeftAttack.SetActive(false);
    }

    void Update()
    {
        RegisterInputs();
        DetermineMovement();
        HorizontalMovement();
        HealthDisplay.text = "HP: " + PlayerHealthLeft;

        if (jumpInput && JumpTimer <= 0) Jumping();
        //if (fallInput) Falling();
        if (LeftWallCling || RightWallCling) ClimbingWalls();

        AttackTimeLeft -= Time.deltaTime;
        if (AttackTimeLeft <= 0) CanAttack = true;

        JumpTimer -= Time.deltaTime;

        SetAnimations();
        PlayAnimation();
    }

    void RegisterInputs()
    {
        leftInput = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        rightInput = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        jumpInput = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
        fallInput = Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S);
        //if (leftInput || rightInput) CollisionCheck = true;
        if (Input.GetKeyDown(KeyCode.Space)) Attack();
        
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
            facingRight = false;
            isMoving = true;
            if (CanJump) transform.position += new Vector3(-GroundSpeed * Time.deltaTime, 0);
            else transform.position += new Vector3((-GroundSpeed / 2) * Time.deltaTime, 0, 0);

        }
        else if (movingRight && !WallDetection(Vector2.right, RaycastDistance) && !LeftWallCling && !RightWallCling)
        {
            facingRight = true;
            isMoving = true;
            if (CanJump) transform.position += new Vector3(GroundSpeed * Time.deltaTime, 0, 0);
            else transform.position += new Vector3((GroundSpeed / 2) * Time.deltaTime, 0, 0);
        }
        else isMoving = false;
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
            isJumping = true;
            currentGravity = ArtificialGravity;
            if (LeftWallCling)
            {
                rb.AddForce(new Vector2(WallJumpForce, 0));
                facingRight = true;
            }
            if (RightWallCling)
            {
                rb.AddForce(new Vector2(-WallJumpForce, 0));
                facingRight = false;
            }
            LeftWallCling = false;
            RightWallCling = false;
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2 (0, JumpForce));
        }
        CanJump = false;
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


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "HealthBoost")
        {
            PlayerHealthLeft += 1;
            Destroy(other.gameObject);
        }
    }

void OnCollisionExit2D(Collision2D collision)
    {
        //CheckCollision(collision);
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


        //if (direction.x < -0.8f && leftInput) LeftWallCling = true;
        //if (direction.x > 0.8f && rightInput) RightWallCling = true;
        if (direction.y > 0.5f || direction.y < -0.5f)
        {
            LeftWallCling = false;
            RightWallCling = false;
        }

        if (!CanJump && collision.gameObject.CompareTag("Wall") && direction.y < 1 && JumpTimer <= 0)
        {
            print(collision.gameObject.name);
            CanJump = true;
            isJumping = false;
            JumpTimer = MaxJumpTime;
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

    void Attack()
    {
        AttackTimeLeft = AttackTimer;
        CanAttack = false;
        if (facingRight)RightAttack.SetActive(true);
        else LeftAttack.SetActive(true);
    }

    public void ResetJump()
    {
        CanJump = true;
    }

    public void PlayerGetsHurt()
    {
        PlayerHealthLeft -= 1;
        UpdateDisplay();
        if (PlayerHealthLeft <= 0)
        {
            PlayerDies();
        }
    }

    void PlayerDies()
    {
        PlayerSpawnController.Instance.PlayerDied();
        gameObject.SetActive(false);
    }

    public Text HealthDisplay;
    public void UpdateDisplay()
    {
        HealthDisplay.text = "HP: " + PlayerHealthLeft;
    }

    public void Respawn()
    {
        PlayerHealthLeft = MaxPlayerHealth;
    }







    public void SetAnimations()
    {
        if (RightWallCling) SetAnimaion = 4;
        else if (LeftWallCling) SetAnimaion = 8;
        else if (facingRight && !CanAttack && !isJumping) SetAnimaion = 9;
        else if (!facingRight && !CanAttack && !isJumping) SetAnimaion = 11;
        else if (facingRight && !CanAttack && isJumping) SetAnimaion = 10;
        else if (!facingRight && !CanAttack && isJumping) SetAnimaion = 12;
        else if (facingRight && isJumping) SetAnimaion = 3;
        else if (!facingRight && isJumping) SetAnimaion = 7;
        else if (facingRight && isMoving && !isJumping) SetAnimaion = 2;
        else if (!facingRight && isMoving && !isJumping) SetAnimaion = 6;
        else if (facingRight && !isMoving && !isJumping) SetAnimaion = 1;
        else if (!facingRight && !isMoving && !isJumping) SetAnimaion = 5;
    }

    public void PlayAnimation()
    {
        switch (SetAnimaion)
        {
            case 12:
                StandRight.SetActive(false);
                StandLeft.SetActive(false);
                WalkRight.SetActive(false);
                WalkLeft.SetActive(false);
                JumpRight.SetActive(false);
                JumpLeft.SetActive(false);
                ClingRight.SetActive(false);
                ClingLeft.SetActive(false);
                AttackRight.SetActive(false);
                AttackLeft.SetActive(false);
                JumpAttackRight.SetActive(false);
                JumpAttackLeft.SetActive(true);
                break;
            case 11:
                StandRight.SetActive(false);
                StandLeft.SetActive(false);
                WalkRight.SetActive(false);
                WalkLeft.SetActive(false);
                JumpRight.SetActive(false);
                JumpLeft.SetActive(false);
                ClingRight.SetActive(false);
                ClingLeft.SetActive(false);
                AttackRight.SetActive(false);
                AttackLeft.SetActive(true);
                JumpAttackRight.SetActive(false);
                JumpAttackLeft.SetActive(false);
                break;
            case 10:
                StandRight.SetActive(false);
                StandLeft.SetActive(false);
                WalkRight.SetActive(false);
                WalkLeft.SetActive(false);
                JumpRight.SetActive(false);
                JumpLeft.SetActive(false);
                ClingRight.SetActive(false);
                ClingLeft.SetActive(false);
                AttackRight.SetActive(false);
                AttackLeft.SetActive(false);
                JumpAttackRight.SetActive(true);
                JumpAttackLeft.SetActive(false);
                break;
            case 9:
                StandRight.SetActive(false);
                StandLeft.SetActive(false);
                WalkRight.SetActive(false);
                WalkLeft.SetActive(false);
                JumpRight.SetActive(false);
                JumpLeft.SetActive(false);
                ClingRight.SetActive(false);
                ClingLeft.SetActive(false);
                AttackRight.SetActive(true);
                AttackLeft.SetActive(false);
                JumpAttackRight.SetActive(false);
                JumpAttackLeft.SetActive(false);
                break;
            case 8:
                StandRight.SetActive(false);
                StandLeft.SetActive(false);
                WalkRight.SetActive(false);
                WalkLeft.SetActive(false);
                JumpRight.SetActive(false);
                JumpLeft.SetActive(false);
                ClingRight.SetActive(false);
                ClingLeft.SetActive(true);
                AttackRight.SetActive(false);
                AttackLeft.SetActive(false);
                JumpAttackRight.SetActive(false);
                JumpAttackLeft.SetActive(false);
                break;
            case 7:
                StandRight.SetActive(false);
                StandLeft.SetActive(false);
                WalkRight.SetActive(false);
                WalkLeft.SetActive(false);
                JumpRight.SetActive(false);
                JumpLeft.SetActive(true);
                ClingRight.SetActive(false);
                ClingLeft.SetActive(false);
                AttackRight.SetActive(false);
                AttackLeft.SetActive(false);
                JumpAttackRight.SetActive(false);
                JumpAttackLeft.SetActive(false);
                break;
            case 6:
                StandRight.SetActive(false);
                StandLeft.SetActive(false);
                WalkRight.SetActive(false);
                WalkLeft.SetActive(true);
                JumpRight.SetActive(false);
                JumpLeft.SetActive(false);
                ClingRight.SetActive(false);
                ClingLeft.SetActive(false);
                AttackRight.SetActive(false);
                AttackLeft.SetActive(false);
                JumpAttackRight.SetActive(false);
                JumpAttackLeft.SetActive(false);
                break;
            case 5:
                StandRight.SetActive(false);
                StandLeft.SetActive(true);
                WalkRight.SetActive(false);
                WalkLeft.SetActive(false);
                JumpRight.SetActive(false);
                JumpLeft.SetActive(false);
                ClingRight.SetActive(false);
                ClingLeft.SetActive(false);
                AttackRight.SetActive(false);
                AttackLeft.SetActive(false);
                JumpAttackRight.SetActive(false);
                JumpAttackLeft.SetActive(false);
                break;
            case 4:
                StandRight.SetActive(false);
                StandLeft.SetActive(false);
                WalkRight.SetActive(false);
                WalkLeft.SetActive(false);
                JumpRight.SetActive(false);
                JumpLeft.SetActive(false);
                ClingRight.SetActive(true);
                ClingLeft.SetActive(false);
                AttackRight.SetActive(false);
                AttackLeft.SetActive(false);
                JumpAttackRight.SetActive(false);
                JumpAttackLeft.SetActive(false);
                break;
            case 3:
                StandRight.SetActive(false);
                StandLeft.SetActive(false);
                WalkRight.SetActive(false);
                WalkLeft.SetActive(false);
                JumpRight.SetActive(true);
                JumpLeft.SetActive(false);
                ClingRight.SetActive(false);
                ClingLeft.SetActive(false);
                AttackRight.SetActive(false);
                AttackLeft.SetActive(false);
                JumpAttackRight.SetActive(false);
                JumpAttackLeft.SetActive(false);
                break;
            case 2:
                StandRight.SetActive(false);
                StandLeft.SetActive(false);
                WalkRight.SetActive(true);
                WalkLeft.SetActive(false);
                JumpRight.SetActive(false);
                JumpLeft.SetActive(false);
                ClingRight.SetActive(false);
                ClingLeft.SetActive(false);
                AttackRight.SetActive(false);
                AttackLeft.SetActive(false);
                JumpAttackRight.SetActive(false);
                JumpAttackLeft.SetActive(false);
                break;
            case 1:
                StandRight.SetActive(true);
                StandLeft.SetActive(false);
                WalkRight.SetActive(false);
                WalkLeft.SetActive(false);
                JumpRight.SetActive(false);
                JumpLeft.SetActive(false);
                ClingRight.SetActive(false);
                ClingLeft.SetActive(false);
                AttackRight.SetActive(false);
                AttackLeft.SetActive(false);
                JumpAttackRight.SetActive(false);
                JumpAttackLeft.SetActive(false);
                break;
            default:
                StandRight.SetActive(true);
                StandLeft.SetActive(false);
                WalkRight.SetActive(false);
                WalkLeft.SetActive(false);
                JumpRight.SetActive(false);
                JumpLeft.SetActive(false);
                ClingRight.SetActive(false);
                ClingLeft.SetActive(false);
                AttackRight.SetActive(false);
                AttackLeft.SetActive(false);
                JumpAttackRight.SetActive(false);
                JumpAttackLeft.SetActive(false);
                break;
        }
    }
}
