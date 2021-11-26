using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoltShot : MonoBehaviour
{

 
    public GameObject NormalEye, RedEye;
    public Transform Synapse;
    private Quaternion originalRot;
   
    public Vector3 playerTransform;
    public LayerMask layerMask;
    public Quaternion AngleRot;

    private void Start()
    {

        originalRot = Synapse.rotation;
       
       
    }
    
    private void Shoot()
    {

   

            RaycastHit2D hit;
            if(hit = Physics2D.Raycast(Synapse.position, playerTransform.position - Synapse.position, Mathf.Infinity, layerMask))
            {

                Debug.DrawLine(Synapse.position, hit.point, Color.green, 10f);

                if(hit.collider.tag == "Player")
                {

                    Vector3 tempVector = playerTransform.position;
                    tempVector.z = 0;

                    tempVector.x = tempVector.x - Synapse.position.x;
                    tempVector.y = tempVector.y - Synapse.position.y;

                    float dif = Mathf.Atan2(tempVector.y, tempVector.x) * Mathf.Rad2Deg;
                    AngleRot = Quaternion.Euler(0, 0, dif - 90);

               
                  

                }
         

            }

        }
        

    

  

}
