using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public int PlayerHealth;
    public bool IsDuplicate;
    public Text HealthText;
    private float invincibilityTimer;
    private GameManager gm;

    void Start()
    {

        if(gm == null)
            gm = GameManager.main;

        if(HealthText)
        {

            HealthText.text = "Player Health: " + PlayerHealth;

        }

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

    /*private void OnCollisionStay2D(Collision2D col)
    {

        if(col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {

            GameManager.DeathType tempDeathType;

            Debug.Log("Took damage from: " + col.gameObject.name);
            
            if(col.gameObject.name.Contains("Acid"))
            {

                tempDeathType = GameManager.DeathType.Acid;
                Debug.Log("Acid damage");

            }
            else
            {

                tempDeathType = GameManager.DeathType.Normal;
                
                Debug.Log("Normal damage");

            }

            TakeDamage(tempDeathType);

        }

    }*/

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

        if(HealthText)
        {

            HealthText.text = "Player Health: " + PlayerHealth;

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
            else
                GameManager.main.DupeDied();

        }

    }

}
