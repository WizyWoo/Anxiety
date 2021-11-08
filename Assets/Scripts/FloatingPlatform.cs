using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{

    public Transform Target;
    public float Speed;

    private void OnTriggerStay2D(Collider2D col)
    {

        if(col.gameObject.tag == "Player")
        {

            transform.Translate(Vector2.MoveTowards(transform.position, Target.position, Speed));

        }

    }

}
