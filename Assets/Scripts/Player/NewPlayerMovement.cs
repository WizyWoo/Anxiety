using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{

    //Movement
    [Range(0, 100
    )]
    public float moveSpeed;
    [Tooltip("should be between 0 and 1")]
    public float controlPowerInAir, friction, DashThreshold;
    public bool MovementEnabled, Dashing;
    [Tooltip("should be the Player physicsMaterial 2D")]
    public PhysicsMaterial2D PM2D;
    private float inAir, loseSpeed, dashCD;
    private Vector2 movementVector;
    //Jumping
    private float playerControlPower, speedMultiplier, xMoveDir;
    //GroundCheck
    private float yGroundCheckOffset, groundCheckDist;
    [SerializeField]
    private bool isGrounded;
    private LayerMask maskPlayer;
    //References
    private CapsuleCollider2D playerCollider;
    private Rigidbody2D rb2D;
    public SpriteRenderer PlayerSpriteRenderer; 
    private PlayerAbilites pa;

    void Start()
    {

        rb2D = gameObject.GetComponent<Rigidbody2D>();
        playerCollider = gameObject.GetComponent<CapsuleCollider2D>();
        maskPlayer = ~((1 << LayerMask.NameToLayer("Player")) + (1 << LayerMask.NameToLayer("Air")) + (1 << LayerMask.NameToLayer("Enemy")));
        rb2D.sharedMaterial = PM2D;
        playerCollider.sharedMaterial = PM2D;
        speedMultiplier = 1f;
        yGroundCheckOffset = (-playerCollider.size.y * 0.002f) + playerCollider.offset.y;
        groundCheckDist = 0.5f * transform.localScale.y;
        pa = gameObject.GetComponent<PlayerAbilites>();
        
    }

    public void DisableMovement(float duration)
    {

        Dashing = true;
        dashCD = duration;

    }

    void Update()
    {

        RaycastHit2D hit2D;

        if(rb2D.velocity.magnitude < DashThreshold) Dashing = false;

        if (hit2D = Physics2D.CircleCast(transform.position + new Vector3(0, yGroundCheckOffset, 0), 0.5f * playerCollider.size.x * transform.localScale.y, new Vector2(0, -1), groundCheckDist, maskPlayer))
        {
            if (hit2D.collider.isTrigger != true)
            {

                isGrounded = true;
                pa.IsGrounded = true;

                playerControlPower = 1;

            }
            else {isGrounded = false; pa.IsGrounded = false;}

        }
        else {isGrounded = false; pa.IsGrounded = false;}

        if (isGrounded == false)
        {

            playerControlPower = controlPowerInAir;
            PM2D.friction = 0;
            inAir = 1;
            rb2D.sharedMaterial = PM2D;
            playerCollider.sharedMaterial = PM2D;
            if (rb2D.velocity.x >= (moveSpeed * speedMultiplier) && Input.GetAxis("Horizontal") > 0) xMoveDir = 0;
            else
            if (rb2D.velocity.x <= (-moveSpeed * speedMultiplier) && Input.GetAxis("Horizontal") < 0) xMoveDir = 0;
            else xMoveDir = Input.GetAxis("Horizontal");

        }
        else
        {

            playerControlPower = 1f;
            PM2D.friction = friction;
            inAir = 0;
            rb2D.sharedMaterial = PM2D;
            playerCollider.sharedMaterial = PM2D;
            if (rb2D.velocity.x >= (moveSpeed * speedMultiplier) && Input.GetAxis("Horizontal") > 0) {xMoveDir = 0; inAir = 1;}
            else
            if (rb2D.velocity.x <= (-moveSpeed * speedMultiplier) && Input.GetAxis("Horizontal") < 0) {xMoveDir = 0; inAir = 1;}
            else xMoveDir = Input.GetAxis("Horizontal");

        }

        if(Input.GetAxisRaw("Horizontal") == -1)
        {

            PlayerSpriteRenderer.flipX = true;
            loseSpeed = 1;

        }
        else if(Input.GetAxisRaw("Horizontal") == 1)
        {

            PlayerSpriteRenderer.flipX = false;
            loseSpeed = 1;

        }

        if(!Dashing)
            movementVector = new Vector2(xMoveDir * playerControlPower * moveSpeed + (rb2D.velocity.x * inAir), rb2D.velocity.y);
        else
            movementVector = new Vector2((rb2D.velocity.x * loseSpeed) + (xMoveDir * controlPowerInAir), rb2D.velocity.y);

        if(!MovementEnabled)
        {

            rb2D.simulated = MovementEnabled;
            rb2D.velocity = Vector2.zero;
            movementVector = Vector2.zero;

        }
        else
            rb2D.simulated = MovementEnabled;

        rb2D.velocity = movementVector;

        dashCD -= Time.deltaTime;
        
    }

    public void OnCollisionStay2D(Collision2D col)
    {

        if(col.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {

            if(dashCD < 0)
            {

                Dashing = false;

            }

        }

    }

}