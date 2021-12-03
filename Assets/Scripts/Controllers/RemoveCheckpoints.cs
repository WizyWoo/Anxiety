using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCheckpoints : MonoBehaviour
{

    void Start()
    {

        if(Checkpoint.CheckPointController)
        {

            Checkpoint.CheckPointController.FinishedLevel();

        }
        
    }

}
