using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    public float WalkModifier;
    private int animMode;
    private float walking;
    private bool grounded;
    private Animator animator;
    private PlayerMovement movement;
    private Rigidbody2D rb2D;

    private void Start()
    {

        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        rb2D = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {

        grounded = movement.IsGrounded;
        
        if(grounded)
        {

            if(Mathf.Abs(rb2D.velocity.x) > 0)
            {

                animMode = 1;
                walking += (Mathf.Abs(rb2D.velocity.x) * WalkModifier) * Time.deltaTime;
                walking = Mathf.Clamp(walking, 0, 1);

            }
            else
            {

                animMode = 0;

            }

        }
        else
        {

            if(rb2D.velocity.y > 0)
            {

                animMode = 2;

            }
            else
            {

                animMode = 3;

            }

        }
        
        animator.SetFloat("Walking", walking);
        animator.SetInteger("AnimMode", animMode);

        if(walking >= 1)
            walking = 0;

    }

}
