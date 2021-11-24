using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{

    public int Number;

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.S))
        {

            SaveAndLoad.SaveStats(false, Number);

        }
        else if(Input.GetKeyDown(KeyCode.L))
        {

            Debug.Log(SaveAndLoad.LoadStats().Deaths);

        }
        else if(Input.GetKeyDown(KeyCode.W))
        {

            SaveAndLoad.WipeStats();

        }
        else if(Input.GetKeyDown(KeyCode.P))
        {

            SaveAndLoad.SaveStats(true, Number);

        }

    }

}
