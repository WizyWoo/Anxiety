using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{

    public GameManager.DeathType DamageType;
    public bool SelfDieOnCol;

    private void OnCollisionEnter2D(Collision2D col)
    {

        if(col.gameObject.tag == "Player")
        {

            col.gameObject.GetComponent<PlayerController>().TakeDamage(DamageType);

        }

        if(SelfDieOnCol)
            Destroy(gameObject);

    }

}
