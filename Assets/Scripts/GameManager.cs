using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager main;
    public GameObject CursorLight;
    public GameObject Background;
    public GameObject Player;
    public GameObject Dupe;
    public bool IsSplit;
    private float backgroundOffsetX;
    private Vector3 mousePosition;
    private bool playerSwapped;
    private GameObject cameraObject, activePlayer;

    void Start()
    {

        main = this;

        if(!Player)
            Player = GameObject.FindGameObjectWithTag("Player");
        activePlayer = Player;
        cameraObject = Camera.main.gameObject;

        /*if(Level1)
            StartCoroutine(BiPolarVision());*/

    }

    void Update()
    {

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CursorLight.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
        
        cameraObject.transform.position = new Vector3(activePlayer.transform.position.x, activePlayer.transform.position.y, -10);

        if(Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void PlayerDied()
    {

        Destroy(Player);
        if(Dupe)
            Destroy(Dupe);

    }

    public void SwapPlayer()
    {

        playerSwapped = !playerSwapped;

        if(playerSwapped)
        {

            Player.GetComponent<PlayerController>().GoDormant();
            Dupe.GetComponent<PlayerController>().WakeUp();
            activePlayer = Dupe;

        }
        else
        {

            Player.GetComponent<PlayerController>().WakeUp();
            Dupe.GetComponent<PlayerController>().GoDormant();
            activePlayer = Player;

        }

    }

    /*private IEnumerator BiPolarVision()
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

    }*/

}
