using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Movement
    [Header("Movement")]
    [Range(0, 50)]
    public float MoveSpeed;
    [Tooltip("should be between 0 and 1")]
    public float ControlPowerInAir, Friction;
    public float SpeedDampening, CoyoteTime;
    public bool SprintOnOff, ShouldSlide;
    public bool MovementEnabled;
    [Tooltip("should be the Player physicsMaterial 2D")]
    public PhysicsMaterial2D PM2D;
    private float yVel, jumpPower, damping;
    private Vector2 movementVector;
    //Jumping
    public float JumpHeight, FallSpeed, DashPower, WidthReduction;
    public bool JumpOnOff, GroundedOverride;
    private float playerControlPower, speedMultiplier, xMoveDir, coyote, coyoteCD;
    private int jumpOnOff, doubleJumpAvailible;
    private bool jumpInput;
    //GroundCheck
    [Header("Do not touch!")]
    public bool IsGrounded;
    private float yGroundCheckOffset, groundCheckDist;
    private LayerMask maskPlayer;
    //References
    public GameObject DashEffect;
    public SpriteRenderer PlayerSprite;
    private CapsuleCollider2D playerCollider;
    private Rigidbody2D rb2D;
    private AudioManager audioManager;

    void Start()
    {

        rb2D = gameObject.GetComponent<Rigidbody2D>();
        playerCollider = gameObject.GetComponent<CapsuleCollider2D>();
        maskPlayer = ~((1 << LayerMask.NameToLayer("Player")) + (1 << LayerMask.NameToLayer("Air")) + (1 << LayerMask.NameToLayer("Enemy")));
        rb2D.sharedMaterial = PM2D;
        playerCollider.sharedMaterial = PM2D;
        speedMultiplier = 1f;
        yGroundCheckOffset = -0.002f + playerCollider.offset.y;
        groundCheckDist = 0.5f * transform.localScale.y;
        jumpPower = 1f;
        if(!audioManager)
            audioManager = AudioManager.main;

    }

    void Update()
    {

        if(JumpOnOff)
            if (Input.GetAxisRaw("Vertical") != 0 && Input.GetAxisRaw("Vertical") != -1 || Input.GetKeyDown(KeyCode.Space))
                jumpInput = true;
            else
                jumpInput = false;

        if(coyoteCD > 0)
        {

            coyote = 0;

        }

        RaycastHit2D hit2D;
        ShouldSlide = false;

        if(!GroundedOverride)
        {

            if(hit2D = Physics2D.CircleCast(transform.position + new Vector3(0, yGroundCheckOffset, 0), 0.5f * (playerCollider.size.x * WidthReduction) * transform.localScale.y, new Vector2(0, -1), groundCheckDist, maskPlayer))
            {

                if(hit2D.collider.isTrigger == false)
                {

                    doubleJumpAvailible = 1;
                    coyote = CoyoteTime;

                    playerControlPower = 1;
                    IsGrounded = true;
                    if (jumpInput && rb2D.velocity.y < JumpHeight)
                    {

                        jumpOnOff = 1;

                    }

                }
                else if(jumpInput && rb2D.velocity.y < JumpHeight && coyote > 0)
                {

                    jumpOnOff = 1;
                    IsGrounded = true;

                }
                else
                {
                    
                    IsGrounded = false;
                    rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y - (FallSpeed * Time.deltaTime));

                }

            }
            else if(jumpInput && rb2D.velocity.y < JumpHeight && coyote > 0)
            {

                jumpOnOff = 1;
                IsGrounded = true;

            }
            else
            {
                
                IsGrounded = false;
                rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y - (FallSpeed * Time.deltaTime));

            }

        }
        else
            IsGrounded = false;

        if(jumpOnOff == 1)
        {

            coyote = 0;
            coyoteCD = 0.1f;

        }

        if(IsGrounded == false)
        {

            playerControlPower = ControlPowerInAir;
            PM2D.friction = 0;
            rb2D.sharedMaterial = PM2D;
            playerCollider.sharedMaterial = PM2D;

        }
        else
        {

            playerControlPower = 1f;
            PM2D.friction = Friction;
            rb2D.sharedMaterial = PM2D;
            playerCollider.sharedMaterial = PM2D;

        }

        if(SprintOnOff)
        {

            if (Input.GetKey(KeyCode.LeftShift)) speedMultiplier = 1.6f;
            else speedMultiplier = 1f;

        }

        //Checks if you're exceeding your max movement speed, if yes, player cannot accelerate further
        if(rb2D.velocity.x >= (MoveSpeed * speedMultiplier) && Input.GetAxis("Horizontal") > 0) xMoveDir = 0;
        else if(rb2D.velocity.x <= (-MoveSpeed * speedMultiplier) && Input.GetAxis("Horizontal") < 0) xMoveDir = 0;
        else xMoveDir = Input.GetAxis("Horizontal");

        if(Input.GetAxisRaw("Horizontal") == -1)
            PlayerSprite.flipX = true;
        else if(Input.GetAxisRaw("Horizontal") == 1)
            PlayerSprite.flipX = false;

        yVel = rb2D.velocity.y - JumpHeight;

        //Checks if you're allowed to move in the players desired direction and if you're grounded, then checks if you are trying to move in the opposite direction of your current x velocity, 
        //if yes, the velocity in the x direction is set to 0 and starts moving in the players desired direction. If player is grounded but not moving slow movement until stopped.
        //if not grounded then player will fly freely through the air with reduced control.
        if(xMoveDir != 0 && IsGrounded)
        {

            if(xMoveDir < 0 && rb2D.velocity.x > 0)
                movementVector = new Vector2(speedMultiplier * xMoveDir * playerControlPower, rb2D.velocity.y + (-yVel * jumpOnOff * jumpPower));
            else if(xMoveDir > 0 && rb2D.velocity.x < 0)
                movementVector = new Vector2(speedMultiplier * xMoveDir * playerControlPower, rb2D.velocity.y + (-yVel * jumpOnOff * jumpPower));
            else movementVector = new Vector2(rb2D.velocity.x + (speedMultiplier * xMoveDir * playerControlPower), rb2D.velocity.y + (-yVel * jumpOnOff * jumpPower));

        }
        else if(IsGrounded)
        {

            movementVector = new Vector2(rb2D.velocity.x * (damping / SpeedDampening), rb2D.velocity.y + (-yVel * jumpOnOff * jumpPower));

        }
        else
        {

            movementVector = new Vector2(rb2D.velocity.x + (speedMultiplier * xMoveDir * playerControlPower), rb2D.velocity.y + (-yVel * jumpOnOff * jumpPower));

        }

        if (Input.GetAxisRaw("Horizontal") == 0)
        {

            if (ShouldSlide == false && IsGrounded)
            {

                damping -= Time.deltaTime;
                damping = Mathf.Clamp(damping, 0, SpeedDampening);

                movementVector = new Vector2(rb2D.velocity.x * (damping / SpeedDampening), rb2D.velocity.y + (-yVel * jumpOnOff * jumpPower));

            }

        }
        else damping = SpeedDampening;

        if(!IsGrounded && doubleJumpAvailible == 1 && Input.GetKeyDown(KeyCode.Space))
        {

            doubleJumpAvailible = 0;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dashDir = mousePos - new Vector2(transform.position.x, transform.position.y);
            dashDir = dashDir.normalized;

            movementVector = dashDir * DashPower;
            
            movementVector = new Vector2(Mathf.Clamp(movementVector.x, -DashPower, DashPower), Mathf.Clamp(movementVector.y, -DashPower, DashPower));

            float dif = Mathf.Atan2(movementVector.y, movementVector.x) * Mathf.Rad2Deg;
            Instantiate(DashEffect, transform.position, Quaternion.Euler(0, 0, dif));

        }

        if(!audioManager)
            audioManager.PlayerGrounded = IsGrounded;

        if(!MovementEnabled)
        {

            rb2D.simulated = MovementEnabled;
            rb2D.velocity = Vector2.zero;
            movementVector = Vector2.zero;

        }
        else
            rb2D.simulated = MovementEnabled;

        rb2D.velocity = movementVector;

        //Timers and value resets
        jumpOnOff = 0;

        if(coyote > 0)
            coyote -= Time.deltaTime;

        if(coyoteCD > 0)
            coyoteCD -= Time.deltaTime;

    }

}
