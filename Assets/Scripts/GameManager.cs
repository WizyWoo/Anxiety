using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private float backgroundOffsetX;
    public GameObject CursorLight;
    public GameObject Background;
    public GameObject Player;
    public SpriteMask DangerMask;
    public GameObject PlayerLight;
    public GameObject PlayerDangerLight;
    public GameObject Dupe;
    private Vector3 mousePosition;
    private bool switchColour;
    public float BiPolarWait;
    public bool Level1, isSplit;


    void Start()
    {

        if(Level1)
            StartCoroutine(BiPolarVision());

    }

    void Update()
    {

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CursorLight.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);

        /*backgroundOffsetX = Player.transform.position.x * 0.1f;
        Background.transform.position = new Vector3(Player.transform.position.x - backgroundOffsetX + 50, (Player.transform.position.y * 0.4f) + 20, 0);*/

        

    }

    private IEnumerator BiPolarVision()
    {

        yield return new WaitForSecondsRealtime(BiPolarWait + Random.Range(-3, 1));

        DangerMask.enabled = true;
        PlayerDangerLight.SetActive(true);
        PlayerLight.SetActive(false);

        yield return new WaitForSecondsRealtime(Random.Range(1.5f, 3));

        DangerMask.enabled = false;
        PlayerDangerLight.SetActive(false);
        PlayerLight.SetActive(true);

        StartCoroutine(BiPolarVision());

    }

}
