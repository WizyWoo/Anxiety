using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Converter : MonoBehaviour
{

    public Vector2[] thing1;
    public Vector3[] thing2;
    public LineRenderer LR;

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.L))
        {

            for(int i = 0; i < 94; i++)
            {

                thing2[i] = thing1[i];

            }
            LR.SetPositions(thing2);

        }

    }
}
