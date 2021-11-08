using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{

    public Transform Platform, Target;
    public float Speed;

    private void OnTriggerStay2D(Collider2D col)
    {

        if(col.gameObject.tag == "Player")
        {

            Vector2 temp = Vector2.MoveTowards(new Vector2(Platform.position.x, Platform.position.y), new Vector2(Target.position.x, Target.position.y), Speed);
            Platform.position = new Vector3(temp.x, temp.y, 0);

        }

    }

}
