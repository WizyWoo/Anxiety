using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int PlayerHealth;
    public GameObject PlayerCam;
    public bool IsDuplicate;
    private Rigidbody2D rb2D;
    private float invincibilityTimer;

    void Start()
    {

        rb2D = gameObject.GetComponent<Rigidbody2D>();

    }

    public void GoDormant()
    {

        gameObject.GetComponent<PlayerAbilites>().enabled = false;
        gameObject.GetComponent<PlayerMovement>().enabled = false;

    }

    public void WakeUp()
    {

        gameObject.GetComponent<PlayerAbilites>().enabled = true;
        gameObject.GetComponent<PlayerMovement>().enabled = true;

    }

    public void MarkAsDupe()
    {

        IsDuplicate = true;
        PlayerHealth = 1;

    }

    void Update()
    {

        invincibilityTimer -= Time.deltaTime;
        
    }

    private void OnCollisionStay2D(Collision2D col)
    {

        if(col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
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

        if(!IsDuplicate)
            GameManager.main.PlayerDied();

    }

}
