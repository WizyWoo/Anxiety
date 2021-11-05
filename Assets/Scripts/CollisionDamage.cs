using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D col)
    {

        if(col.gameObject.tag == "Player")
        {

            col.gameObject.GetComponent<PlayerController>().TakeDamage();

        }
        Destroy(gameObject);

    }

}
