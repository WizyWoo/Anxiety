using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilites : MonoBehaviour
{

    public float DashPower, ChargeSpeed, DashCooldown, MaxDashCharge, SplitCooldown;
    public bool DashOnOff, SplitOnOff;
    private float dashCharge, dashCooldownTimer, originalCamSize, resizeSmoother, sizeA, splitCooldownTimer;
    private bool occupied, dashReady, isSplit;
    private Rigidbody2D rb2D;
    private PlayerMovement movemenScript;
    private Camera mainCam;
    private LayerMask nonBlocking;
    public GameManager gm;

    void Start()
    {
        
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        movemenScript = gameObject.GetComponent<PlayerMovement>();
        dashCharge = 1;
        mainCam = Camera.main;
        originalCamSize = mainCam.orthographicSize;
        nonBlocking = ~((1 << LayerMask.NameToLayer("Player")) + (1 << LayerMask.NameToLayer("Air")) + (1 << LayerMask.NameToLayer("Enemy")));
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    void Update()
    {

        if(dashReady && DashOnOff && movemenScript.IsGrounded)
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

        if(splitCooldownTimer < 0 && SplitOnOff && !gm.isSplit)
        {

            if(Input.GetKeyDown(KeyCode.Mouse0))
            {

                Split();

            }

        }

        splitCooldownTimer -= Time.deltaTime;

        if(mainCam.orthographicSize < originalCamSize && !occupied) {resizeCam(); resizeSmoother += Time.deltaTime * 50f;}

        occupied = false;
        
    }

    private void OnCollisionStay2D(Collision2D col)
    {

        if(col.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {

            dashReady = true;

        }

    }

    private void resizeCam()
    {

        mainCam.orthographicSize = Mathf.Lerp(sizeA, originalCamSize, resizeSmoother);

    }

    private void Split()
    {

        GameObject ass = Instantiate(gameObject, transform.position, Quaternion.identity);
        splitCooldownTimer = SplitCooldown;
        gm.isSplit = true;
        ass.GetComponent<PlayerController>().GoDormant();
        gm.Dupe = ass;

    }

    private void Dash(float charge)
    {

        movemenScript.GroundedOverride = true;
        StartCoroutine(GroundedRevert());

        dashCharge = 1;
        dashReady = false;
        occupied = false;
        resizeSmoother = 0;
        sizeA = mainCam.orthographicSize -0.3f;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dashDir = mousePos - new Vector2(transform.position.x, transform.position.y);
        dashDir = dashDir.normalized;

        rb2D.velocity += dashDir * charge * DashPower;

    }

    private IEnumerator GroundedRevert()
    {

        yield return new WaitForSeconds(0.2f);
        movemenScript.GroundedOverride = false;

    }

}