using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxBackground : MonoBehaviour
{

    public float BGParalax, BackParalax, MidParalax, FrontParalax;
    public Transform BG, Back, Mid, Front;
    private Transform player;

    private void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void Update()
    {

        Front.position = player.position * FrontParalax;
        Mid.position = player.position * MidParalax;
        Back.position = player.position * BackParalax;
        BG.position = player.position * BGParalax;

    }

}
