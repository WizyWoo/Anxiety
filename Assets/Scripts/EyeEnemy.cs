using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyeEnemy : MonoBehaviour
{

    public float StressMult, StressCooldown, BlinkSpeed, BlinkInterval;
    public GameObject NormalEye, RedEye, Blinky;
    public Vector3[] EyeRots;
    public Transform TurnyThing;
    private Quaternion originalRot;
    private ShadowStalker shadows;
    private StressBarSimple stressBar;
    private Transform playerTransform;
    private LayerMask layerMask;
    private bool inView;
    private float checkCD;
    private int curEye;

    private void Start()
    {

        originalRot = TurnyThing.rotation;
        shadows = Camera.main.GetComponentInChildren<ShadowStalker>();
        layerMask = ~((1 << LayerMask.NameToLayer("Air")) + (1 << LayerMask.NameToLayer("Enemy")));
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        stressBar = Camera.main.GetComponentInChildren<StressBarSimple>();
        StartCoroutine(blink());

    }
    
    private void Update()
    {

        if(inView)
        {

            NormalEye.SetActive(false);

            RaycastHit2D hit;
            if(hit = Physics2D.Raycast(TurnyThing.position, playerTransform.position - TurnyThing.position, Mathf.Infinity, layerMask))
            {

                Debug.DrawLine(TurnyThing.position, hit.point, Color.green, 10f);

                if(hit.collider.tag == "Player")
                {

                    Vector3 tempVector = playerTransform.position;
                    tempVector.z = 0;

                    tempVector.x = tempVector.x - TurnyThing.position.x;
                    tempVector.y = tempVector.y - TurnyThing.position.y;

                    float dif = Mathf.Atan2(tempVector.y, tempVector.x) * Mathf.Rad2Deg;
                    TurnyThing.rotation = Quaternion.Euler(0, 0, dif - 90);

                    float stress = stressBar.Stress;
                    stress = Mathf.Clamp(stress += StressMult * Time.deltaTime, 0, 1);
                    stressBar.Stress = stress;
                    stressBar.StressDecCD = StressCooldown;

                }
                else
                {

                    inView = false;
                    checkCD = 0.5f;
                    GetComponent<PolygonCollider2D>().enabled = true;
                    StartCoroutine(blink());

                }

            }

        }
        else
        {

            TurnyThing.rotation = originalRot;

        }

        RedEye.SetActive(inView);
        
        if(checkCD > 0)
            checkCD -= Time.deltaTime;

    }

    public IEnumerator blink()
    {

        Blinky.SetActive(true);
        NormalEye.SetActive(false);

        yield return new WaitForSeconds(BlinkSpeed);

        if(EyeRots.Length > 1)
        {

            curEye++;

            TurnyThing.rotation = Quaternion.Euler(0, 0, EyeRots[curEye].z);

        }

        Blinky.SetActive(false);
        NormalEye.SetActive(true);

        yield return new WaitForSeconds(BlinkInterval);

        StartCoroutine(blink());

    }

    private void OnTriggerStay2D(Collider2D col)
    {

        if(col.gameObject.tag == "Player" && checkCD <= 0)
        {

            inView = true;
            GetComponent<PolygonCollider2D>().enabled = false;
            StopAllCoroutines();

        }

    }

}
