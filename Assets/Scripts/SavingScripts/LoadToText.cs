using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadToText : MonoBehaviour
{

    public Text DeathsText;
    
    void Start()
    {
        
        DeathsText.text = "You have died: " + SaveAndLoad.LoadStats().Deaths + " times, Wow :O!";

    }

}
