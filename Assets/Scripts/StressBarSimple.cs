using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressBarSimple : MonoBehaviour
{

    public float StressDecreaseMult, CooldownSpeed, Stress, StressDecCD;
    public Image bar;
    private ShadowStalker shadows;

    private void Start()
    {

        shadows = Camera.main.GetComponentInChildren<ShadowStalker>();
        bar.fillAmount = 0;

    }

    private void Update()
    {

        if(StressDecCD <= 0)
        {

            Stress -= StressDecreaseMult * Time.deltaTime;

        }
        else if(StressDecCD <= 4)
        {

            Stress -= (StressDecreaseMult * 0.15f) * Time.deltaTime;
            StressDecCD -= CooldownSpeed * Time.deltaTime;

        }
        else
        {

            StressDecCD -= CooldownSpeed * Time.deltaTime;

        }

        shadows.T = Stress;
        bar.fillAmount = shadows.Current;

        if(Stress >= 1)
        {

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().TakeDamage(GameManager.DeathType.Stress);
            
        }

    }

}
