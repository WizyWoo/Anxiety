using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public GameObject Ambiance, PlayerWalking;
    public AudioSource[] AudioSources;
    private GameObject player;
    private Rigidbody2D playerRB;

    private void Start()
    {

        foreach (AudioSource source in AudioSources)
        {

            source.volume = 1;
            
        }

    }

    private void Update()
    {

        

    }

    public void ApplySoundLevel(float volume)
    {

        foreach (AudioSource source in AudioSources)
        {

            source.volume = volume;
            
        }

    }

}
