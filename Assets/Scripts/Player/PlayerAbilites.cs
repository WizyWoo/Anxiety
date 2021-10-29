using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilites : MonoBehaviour
{

    public float DashPower, ChargeSpeed, DashCooldown, MaxDashCharge, TeleportCooldown, SplitCooldown;
    public bool DashOnOff, TeleportOnOff, IsGrounded, SplitOnOff;
    [SerializeField]
    private float dashCharge, dashCooldownTimer, originalCamSize, resizeSmoother, sizeA, teleportCooldownTimer, splitCooldownTimer;
    private bool occupied, dashReady, isSplit;
    private Rigidbody2D rb2D;
    private NewPlayerMovement movemenScript;
    private Camera mainCam;
    private LayerMask nonBlocking;
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        movemenScript = gameObject.GetComponent<NewPlayerMovement>();
        dashCharge = 1;
        mainCam = Camera.main;
        originalCamSize = mainCam.orthographicSize;
        nonBlocking = ~((1 << LayerMask.NameToLayer("Player")) + (1 << LayerMask.NameToLayer("Air")) + (1 << LayerMask.NameToLayer("Enemy")));
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {

        if(dashReady && DashOnOff && IsGrounded)
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

        if(teleportCooldownTimer < 0 && TeleportOnOff)
        {

            if(Input.GetKeyDown(KeyCode.Mouse1))
            {

                

            }

        }

        teleportCooldownTimer -= Time.deltaTime;
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

        movemenScript.DisableMovement(0.2f);

        dashCharge = 1;
        dashReady = false;
        occupied = false;
        resizeSmoother = 0;
        sizeA = mainCam.orthographicSize -0.3f;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dashDir = mousePos - transform.position;
        dashDir = dashDir.normalized;

        rb2D.velocity = dashDir * charge * DashPower;

    }

}