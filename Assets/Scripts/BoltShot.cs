using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoltShot : MonoBehaviour
{


    //public GameObject Player;
    public Transform Synapse;
    private Quaternion originalRot;
   
    public Vector3 playerTransform;
    public LayerMask layerMask;
    public Quaternion AngleRot;

    private void Start()
    {

        originalRot = Synapse.rotation;
       
       
    }
    public void Update()
    {

        Vector3 tempVector = playerTransform;
        tempVector.z = 0;

        tempVector.x = tempVector.x - Synapse.position.x;
        tempVector.y = tempVector.y - Synapse.position.y;

        float dif = Mathf.Atan2(tempVector.y * -1, tempVector.x * -1) * Mathf.Rad2Deg;
        AngleRot = Quaternion.Euler(0, 0, dif - 90);

    }
    //public void Shoot()
    //{

   

    //        RaycastHit2D hit;
    //        if(hit = Physics2D.Raycast(Synapse.position, playerTransform - Synapse.position, Mathf.Infinity, layerMask))
    //        {

    //            Debug.DrawLine(Synapse.position, hit.point, Color.green, 10f);

    //            if(hit.collider.tag == "Player")
    //            {

    //            Player = hit.collider.gameObject;
                  

    //            }
         

    //        }

    //    }
        

    

  

}
