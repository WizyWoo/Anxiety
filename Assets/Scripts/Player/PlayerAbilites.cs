using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilites : MonoBehaviour
{

    public enum Abilities
    {

        Split

    }

    public Abilities SelectedAbility;
    public float DashPower, ChargeSpeed, MaxDashCharge, SplitCooldown;
    public bool DashOnOff, SplitOnOff;
    public Transform HoldPosition;
    public GameObject ShadowClone;
    private float dashCharge, originalCamSize, resizeSmoother, sizeA, splitCooldownTimer;
    private bool occupied, isSplit;
    private GameObject holding;
    private Rigidbody2D rb2D;
    private PlayerMovement movemenScript;
    private Camera mainCam;
    private LayerMask nonBlocking;
    private GameManager gm;

    void Start()
    {
        
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        movemenScript = gameObject.GetComponent<PlayerMovement>();
        dashCharge = 1;
        mainCam = Camera.main;
        originalCamSize = mainCam.orthographicSize;
        nonBlocking = ~((1 << LayerMask.NameToLayer("Player")) + (1 << LayerMask.NameToLayer("Air")) + (1 << LayerMask.NameToLayer("Enemy")));
        
        if(gm == null)
            gm = GameManager.main;

    }

    void Update()
    {

        if(holding)
        {

            holding.transform.position = HoldPosition.position;

            Rigidbody2D tempRB;
            if(holding.TryGetComponent<Rigidbody2D>(out tempRB))
            {

                tempRB.velocity = Vector2.zero;

            }

        }

        if(DashOnOff && movemenScript.IsGrounded)
        {
            
            if(Input.GetKey(KeyCode.Space))
            {

                dashCharge += Time.deltaTime * ChargeSpeed;
                mainCam.orthographicSize = originalCamSize + 0.5f - (0.5f * Mathf.Clamp(dashCharge, 0, MaxDashCharge));
                occupied = true;

            }
            if(Input.GetKeyUp(KeyCode.Space))
            {

                dashCharge = Mathf.Clamp(dashCharge, 0, MaxDashCharge);
                Dash(dashCharge);

            }

        }

        if(Input.GetKeyDown(KeyCode.Q))
        {

            if(gm == null)
                gm = GameManager.main;

            switch(SelectedAbility)
            {

                case Abilities.Split:
                if(splitCooldownTimer < 0 && SplitOnOff && !gm.IsSplit)
                {

                    Split();

                }
                else if(gm.IsSplit)
                {

                    gm.SwapPlayer();

                }
                
                break;

            }

        }

        if(Input.GetKeyDown(KeyCode.E))
            PickupObject();

        splitCooldownTimer -= Time.deltaTime;

        if(mainCam.orthographicSize < originalCamSize && !occupied) {resizeCam(); resizeSmoother += Time.deltaTime * 50f;}

        occupied = false;
        
    }

    public void PickupObject(GameObject forcePickup = null)
    {

        if(forcePickup && !holding)
        {

            holding = forcePickup;

        }
        else if(holding)
        {

            holding = null;
            
        }
        else
        {

            Debug.Log("Pickup nearest object");
            holding = gm.Dupe;

        }
        
    }

    private void resizeCam()
    {

        mainCam.orthographicSize = Mathf.Lerp(sizeA, originalCamSize, resizeSmoother);

    }

    private void Split()
    {

        Instantiate(gm.SplitEffect, transform.position, Quaternion.identity);
        GameObject duplicate = Instantiate(ShadowClone, transform.position, Quaternion.identity);
        splitCooldownTimer = SplitCooldown;
        gm.IsSplit = true;
        duplicate.GetComponent<PlayerController>().GoDormant();
        duplicate.GetComponent<PlayerController>().MarkAsDupe();
        gm.Dupe = duplicate;

    }

    private void Dash(float charge)
    {

        movemenScript.GroundedOverride = true;
        StartCoroutine(GroundedRevert());

        dashCharge = 1;
        occupied = false;
        resizeSmoother = 0;
        sizeA = mainCam.orthographicSize -0.3f;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dashDir = mousePos - new Vector2(transform.position.x, transform.position.y);
        dashDir = dashDir.normalized;

        rb2D.velocity = dashDir * charge * DashPower;

    }

    private IEnumerator GroundedRevert()
    {

        yield return new WaitForSeconds(0.2f);
        movemenScript.GroundedOverride = false;

    }

}