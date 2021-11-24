using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{

    public int Deaths;

    public PlayerStats(int deaths, PlayerStats stats, bool overWrite = false)
    {

        if(stats != null)
        {

            Deaths = stats.Deaths + deaths;

        }
        else
        {

            Deaths = deaths;

        }

    }

}
