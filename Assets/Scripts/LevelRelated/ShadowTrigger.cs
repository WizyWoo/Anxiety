using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowTrigger : MonoBehaviour
{
    public GameObject GoodShadow, BadShadow;
    private GameObject Player;
    bool Switch;

    // Start is called before the first frame update
    void Start()
    {
        Switch = false;
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            
            GoodShadow.SetActive(true);
            BadShadow.SetActive(true);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {

            GoodShadow.SetActive(false);
            BadShadow.SetActive(false);
            
        }

    }

}
