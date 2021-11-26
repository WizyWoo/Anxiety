using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{

    public float Volume;

    public SettingsData(float volume = 1)
    {

        Volume = Mathf.Clamp(volume, 0, 1);

    }

}
