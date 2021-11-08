using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{

    public int Deaths;

    public PlayerStats(int previousDeaths)
    {

        Deaths = previousDeaths + 1;

    }

}
