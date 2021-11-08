using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiPolarBehavior : MonoBehaviour
{

    public float ModeSwitchTime, ManicMultiplier, DepMultiplier;
    public Light PlayerLight;
    public Color Manic, Depressed;
    public SpriteMask DangerMask;
    private PlayerAbilites abilites;
    private PlayerController playerController;
    private PlayerMovement movement;
    private GameObject player;
    private float originalMoveSpeed, originalJumpHeight;
    private bool manic;

    private void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        abilites = player.GetComponent<PlayerAbilites>();
        playerController = player.GetComponent<PlayerController>();
        movement = player.GetComponent<PlayerMovement>();
        originalMoveSpeed = movement.MoveSpeed;
        originalMoveSpeed = movement.JumpHeight;

        StartCoroutine(ModeSwitcher());

    }

    private void Update()
    {

        if(manic)
        {

            movement.MoveSpeed = originalMoveSpeed * ManicMultiplier;
            movement.JumpHeight = originalJumpHeight * ManicMultiplier;

        }
        else
        {

            movement.MoveSpeed = originalMoveSpeed * DepMultiplier;
            movement.JumpHeight = originalJumpHeight * DepMultiplier;

        }

    }

    private IEnumerator ModeSwitcher()
    {

        yield return new WaitForSeconds(ModeSwitchTime);

        if(manic)
        {

            //PlayerLight.color = Manic;
            DangerMask.enabled = false;
        
        }
        else
        {

            //PlayerLight.color = Depressed;
            DangerMask.enabled = true;

        }
        manic = !manic;

    }
    
}