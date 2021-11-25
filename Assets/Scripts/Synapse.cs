using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synapse : MonoBehaviour
{

    public SpriteRenderer DamageBar;
    public float SynapseHealth, MaxShake;
    public GameObject SynapseBoom, SynapseDamage;
    private float maxHhealth;
    private bool playerInRange;
    private Vector2 originalPos;

    private void Start()
    {

        originalPos = transform.position;
        maxHhealth = SynapseHealth;
        DamageBar.color = new Color(SynapseHealth / maxHhealth, SynapseHealth / maxHhealth, SynapseHealth / maxHhealth);

    }

    private void Update()
    {

        if(playerInRange && (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Mouse0)))
        {

            SynapseHealth--;
            DamageBar.color = new Color(SynapseHealth / maxHhealth, SynapseHealth / maxHhealth, SynapseHealth / maxHhealth);
            if(SynapseHealth <= 0)
            {

                GameManager.main.PoppedSynapse();
                Instantiate(SynapseBoom, transform.position, Quaternion.identity);
                Destroy(gameObject);

            }
            else
            {

                StartCoroutine(Shake());
                Instantiate(SynapseDamage, transform.position, Quaternion.identity);

            }
            
        }

    }

    private IEnumerator Shake()
    {

        transform.position = new Vector2(originalPos.x + Random.Range(-MaxShake, MaxShake), originalPos.y + Random.Range(-MaxShake, MaxShake));
        yield return new WaitForSeconds(0.05f);
        transform.position = originalPos;

    }

    private void OnTriggerExit2D(Collider2D col)
    {

        if(col.gameObject.tag == "Player") playerInRange = false;

    }

    public void OnTriggerEnter2D(Collider2D col)
    {

        if(col.gameObject.tag == "Player") playerInRange = true;

    }

}
