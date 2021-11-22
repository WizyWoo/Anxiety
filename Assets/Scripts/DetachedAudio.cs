using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachedAudio : MonoBehaviour
{

    [Header("Leave this empty if script is attached to same object as the audioSource")]
    public AudioSource Source;
    public bool AddToManager;

    private void Start()
    {

        if(Source)
            Source.volume = AudioManager.main.GetVolume() * Source.volume;
        else
        {

            Source = GetComponent<AudioSource>();
            Source.volume = AudioManager.main.GetVolume() * Source.volume;

        }

        if(AddToManager)
            AudioManager.main.AddThisToList(Source);
        
    }

}
