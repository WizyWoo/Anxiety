using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{

    public Transform Platform, Target;
    public float Speed;
    private Transform originPoint;
    private float activeTimer;

    private void Start()
    {

        originPoint = new GameObject("OriginPoint").transform;
        originPoint.transform.position = transform.position;

    }

    private void Update()
    {

        activeTimer -= Time.deltaTime;

        if(activeTimer <= 0)
        {

            Vector2 temp = Vector2.MoveTowards(new Vector2(Platform.position.x, Platform.position.y), new Vector2(originPoint.position.x, originPoint.position.y), Speed);
            Platform.position = new Vector3(temp.x, temp.y, 0);

        }

    }

    private void OnTriggerStay2D(Collider2D col)
    {

        if(col.gameObject.tag == "Player")
        {

            Vector2 temp = Vector2.MoveTowards(new Vector2(Platform.position.x, Platform.position.y), new Vector2(Target.position.x, Target.position.y), Speed);
            Platform.position = new Vector3(temp.x, temp.y, 0);

        }

        activeTimer = 0.2f;

    }

}
