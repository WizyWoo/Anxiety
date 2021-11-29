using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    //The AudioManager has 1 extra script for objects that are spawned in during playmode (DetachedAudio). Use that script to add the audio source to the manager or if the audio is gonna play once and dissapear don't turn on the AddToManager bool
    public static AudioManager main {get; private set;}
    public AudioSource Ambiance, PlayerWalking;
    public List<AudioSource> AudioSources;
    public float PlayerWalkingCooldown;
    public bool PlayerGrounded;
    private GameObject player;
    private Rigidbody2D playerRB;
    private float playerWalkingCD;
    [SerializeField, Range(0, 1)]
    private float volume;
    private Dictionary<AudioSource, float> sourceOriginalVol;

    private void Awake()
    {
        
        main = this;
    
    }

    private void Start()
    {

        volume = SaveAndLoad.LoadSettings().Volume;

        sourceOriginalVol = new Dictionary<AudioSource, float>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();

        foreach (AudioSource source in AudioSources)
        {

            sourceOriginalVol.Add(source, source.volume);
            source.volume = volume * source.volume;
            
        }

        Debug.Log(player.name);

    }

    private void Update()
    {

        if(Mathf.Abs(playerRB.velocity.x) > 0.01f && playerWalkingCD <= 0 && PlayerGrounded)
        {

            PlayerWalking.Play();
            playerWalkingCD = PlayerWalkingCooldown;
            playerWalkingCD = PlayerWalkingCooldown;

        }
        else if(Mathf.Abs(playerRB.velocity.x) < 0.1f || !PlayerGrounded)
        {

            playerWalkingCD = 0;
            PlayerWalking.Stop();

        }

        if(playerWalkingCD > 0)
            playerWalkingCD -= Time.deltaTime * Mathf.Abs(playerRB.velocity.x);

    }

    public float GetVolume()
    {

        return volume;

    }

    public void AddThisToList(AudioSource sourceToAdd)
    {

        //AudioSource sourceToAdd = sourceGO.GetComponent<AudioSource>();

        AudioSources.Add(sourceToAdd);
        sourceOriginalVol.Add(sourceToAdd, sourceToAdd.volume);

    }

    public void ApplyVolumeLevel(float volumeChange)
    {

        volume = volumeChange;

        foreach (AudioSource source in AudioSources)
        {

            if(sourceOriginalVol.TryGetValue(source, out float originlVol))
            {

                source.volume = volume * originlVol;

            }
            else
                source.volume = volume;
            
        }

    }

    public void ChangeVolSlider(Slider volSlider)
    {

        ApplyVolumeLevel(volSlider.value);

    }

}
