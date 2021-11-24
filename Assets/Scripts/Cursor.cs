using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{

    public int ParticleAmount;
    public ParticleSystem CursorParticles;
    private ParticleSystem.EmissionModule emission;

    private void Start()
    {

        emission = CursorParticles.emission;

    }

    private void Update()
    {

        if(Input.GetMouseButton(1))
            emission.rateOverTime = ParticleAmount;
        else
            emission.rateOverTime = 0;

    }

}
