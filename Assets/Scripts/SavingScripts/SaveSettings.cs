using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSettings : MonoBehaviour
{

    public void SaveSettingsDat(Slider slider)
    {

        SaveAndLoad.SaveSettings(slider.value);

    }

}
