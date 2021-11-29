using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackCocaineAcidBlood : MonoBehaviour
{

    public GameObject CrackCocaineAcidBloodPrefab;
    public float SpawnDelay, RandomOffsetRange;
    private float spawnDelayTimer;

    void Update()
    {

        spawnDelayTimer -= Time.deltaTime;

        if(spawnDelayTimer < 0)
        {

            spawnDelayTimer = SpawnDelay + Random.Range(-RandomOffsetRange, RandomOffsetRange);
            Instantiate(CrackCocaineAcidBloodPrefab, transform.position, Quaternion.identity);

        }

    }

}
