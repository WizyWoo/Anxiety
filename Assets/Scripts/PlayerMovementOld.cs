using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Movement
    [Range(0, 10)]
    public float moveSpeed;
    [Tooltip("should be between 0 and 1")]
    public float controlPowerInAir, friction;
    public float speedDampening;
    public bool sprintOnOff, shouldSlide;
    public bool MovementEnabled;
    [Tooltip("should be the Player physicsMaterial 2D")]
    public PhysicsMaterial2D PM2D;
    private float yVel, jumpPower, damping;
    private Vector2 movementVector;
    //Jumping
    public float jumpForce;
    private float playerControlPower, speedMultiplier, xMoveDir;
    private int jumpOnOff;
    private bool JumpInput;
    //GroundCheck
    private float yGroundCheckOffset, groundCheckDist;
    [SerializeField]
    private bool isGrounded;
    private LayerMask maskPlayer;
    //References
    private CapsuleCollider2D playerCollider;
    private Rigidbody2D rb2D;
    public SpriteRenderer PlayerSpriteRenderer; 

    public void PopInFinal()
    {
        Debug.Log("popin final");
    }

    void Start()
    {

        rb2D = gameObject.GetComponent<Rigidbody2D>();
        playerCollider = gameObject.GetComponent<CapsuleCollider2D>();
        maskPlayer = ~((1 << LayerMask.NameToLayer("Player")) + (1 << LayerMask.NameToLayer("Air")) + (1 << LayerMask.NameToLayer("Enemy")));
        rb2D.sharedMaterial = PM2D;
        playerCollider.sharedMaterial = PM2D;
        speedMultiplier = 1f;
        yGroundCheckOffset = -playerCollider.size.y * 0.01f;
        groundCheckDist = 0.5f * transform.localScale.y;
        jumpPower = 1f;

    }

    void FixedUpdate()
    {

        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetKey(KeyCode.Space))
            JumpInput = true;
        else
            JumpInput = false;

        RaycastHit2D hit2D;
        shouldSlide = false;

        if (hit2D = Physics2D.CircleCast(transform.position + new Vector3(0, yGroundCheckOffset, 0), 0.5f * playerCollider.size.x * transform.localScale.y, new Vector2(0, -1), groundCheckDist, maskPlayer))
        {
            if (hit2D.collider.isTrigger != true)
            {

                isGrounded = true;

                playerControlPower = 1;
                if (JumpInput && rb2D.velocity.y < jumpForce)
                {

                    jumpOnOff = 1;

                }

            }
            else isGrounded = false;

        }
        else isGrounded = false;

        if (isGrounded == false)
        {

            playerControlPower = controlPowerInAir;
            PM2D.friction = 0;
            rb2D.sharedMaterial = PM2D;
            playerCollider.sharedMaterial = PM2D;

        }
        else
        {

            playerControlPower = 1f;
            PM2D.friction = friction;
            rb2D.sharedMaterial = PM2D;
            playerCollider.sharedMaterial = PM2D;

        }

        if (sprintOnOff)
        {

            if (Input.GetKey(KeyCode.LeftShift)) speedMultiplier = 1.6f;
            else speedMultiplier = 1f;

        }

        if (rb2D.velocity.x >= (moveSpeed * speedMultiplier) && Input.GetAxis("Horizontal") > 0) xMoveDir = 0;
        else
        if (rb2D.velocity.x <= (-moveSpeed * speedMultiplier) && Input.GetAxis("Horizontal") < 0) xMoveDir = 0;
        else xMoveDir = Input.GetAxis("Horizontal");

        if(Input.GetAxisRaw("Horizontal") == -1)
            PlayerSpriteRenderer.flipX = true;
        else if(Input.GetAxisRaw("Horizontal") == 1)
            PlayerSpriteRenderer.flipX = false;

        yVel = rb2D.velocity.y - jumpForce;

        movementVector = new Vector2(rb2D.velocity.x + (speedMultiplier * xMoveDir * playerControlPower), rb2D.velocity.y + (-yVel * jumpOnOff * jumpPower));

        if (Input.GetAxisRaw("Horizontal") == 0)
        {

            if (shouldSlide == false && isGrounded)
            {

                damping -= Time.deltaTime;
                damping = Mathf.Clamp(damping, 0, speedDampening);

                movementVector = new Vector2(rb2D.velocity.x * (damping / speedDampening), rb2D.velocity.y + (-yVel * jumpOnOff * jumpPower));

            }

        }
        else damping = speedDampening;

        jumpOnOff = 0;

        if(!MovementEnabled)
        {

            rb2D.simulated = MovementEnabled;
            rb2D.velocity = Vector2.zero;
            movementVector = Vector2.zero;

        }
        else
            rb2D.simulated = MovementEnabled;

        rb2D.velocity = movementVector;

    }

}
