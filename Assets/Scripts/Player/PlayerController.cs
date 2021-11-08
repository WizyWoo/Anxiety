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
    private GameManager gm;

    void Start()
    {

        rb2D = gameObject.GetComponent<Rigidbody2D>();

        if(!gm)
            gm = GameManager.main;

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

            GameManager.DeathType tempDeathType;

            Debug.Log("Took damage from: " + col.gameObject.name);
            
            if(col.gameObject.name.Contains("acid"))
            {

                tempDeathType = GameManager.DeathType.Acid;

            }
            else
            {

                tempDeathType = GameManager.DeathType.Normal;

            }

            TakeDamage(tempDeathType);

        }

    }

    public void TakeDamage(GameManager.DeathType damageFrom = GameManager.DeathType.Normal)
    {

        if(invincibilityTimer < 0)
        {

            invincibilityTimer = 1.2f;
            PlayerHealth -= 1;

        }

        if(PlayerHealth <= 0)
        {

            Die(damageFrom);

        }

    }

    public void Die(GameManager.DeathType deathType)
    {

        if(!gm)
        {

            Destroy(gameObject);

        }
        else
        {

            if(!IsDuplicate)
                GameManager.main.PlayerDied(deathType);

        }

    }

}
