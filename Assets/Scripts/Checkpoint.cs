using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public Vector3 CheckpointPosition;

    void Start()
    {
        
        DontDestroyOnLoad(gameObject);

    }

    void Update()
    {
        
    }
}
