using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{

    public ParticleSystem CursorParticles;

    private void Update()
    {

        if(Input.GetMouseButton(1))
            CursorParticles.enableEmission = true;
        else
            CursorParticles.enableEmission = false;

    }

}
