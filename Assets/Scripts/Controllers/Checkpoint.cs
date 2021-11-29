using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public static Checkpoint CheckPointController {get; set;}
    public Vector3 CheckpointPosition;
    public int CheckpointNR;

    private void Awake()
    {

        CheckPointController = this;

    }

    public void CheckpointReached(Vector3 checkpointPos)
    {

        CheckpointPosition = checkpointPos;
        CheckpointNR++;

    }

    public void FinishedLevel()
    {

        CheckpointNR = 0;
        CheckpointPosition = Vector3.zero;

    }

    void Start()
    {
        
        DontDestroyOnLoad(gameObject);

    }

}
