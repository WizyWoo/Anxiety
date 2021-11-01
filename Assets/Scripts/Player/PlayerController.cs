using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public int PlayerHealth;
    public GameObject PlayerCam;
    private Rigidbody2D rb2D;
    private float invincibilityTimer;

    void Start()
    {

        rb2D = gameObject.GetComponent<Rigidbody2D>();

    }

    public void GoDormant()
    {

        gameObject.GetComponent<PlayerAbilites>().enabled = false;
        gameObject.GetComponent<NewPlayerMovement>().enabled = false;
        PlayerCam.SetActive(false);

    }

    public void WakeUp()
    {



    }

    void Update()
    {

        invincibilityTimer -= Time.deltaTime;
        
    }

    private void OnCollisionStay2D(Collision2D col)
    {

        if(col.gameObject.tag == "Hurty")
        {

            TakeDamage();

        }

    }

    public void TakeDamage()
    {

        if(invincibilityTimer < 0)
        {

            invincibilityTimer = 1.2f;
            PlayerHealth -= 1;

        }

        if(PlayerHealth <= 0)
        {

            Die();

        }

    }

    public void Die()
    {

        Debug.Log("ded");

    }

}
