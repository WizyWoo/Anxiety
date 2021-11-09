using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public enum DeathType
    {

        Normal,
        Acid

    }

    public static GameManager main;
    public GameObject CursorLight, NormalDeath, AcidDeath, DeathMessageObject;
    public string AcidDeathMSG, NormalDeathMSG;
    [HideInInspector]
    public GameObject Player;
    [HideInInspector]
    public GameObject Dupe;
    [HideInInspector]
    public bool IsSplit;
    private float backgroundOffsetX;
    private Vector3 mousePosition;
    private bool playerSwapped;
    private GameObject cameraObject, activePlayer;

    private void Awake() => main = this;

    void Start()
    {

        if(!Player)
            Player = GameObject.FindGameObjectWithTag("Player");
        activePlayer = Player;
        cameraObject = Camera.main.gameObject;

        /*if(Level1)
            StartCoroutine(BiPolarVision());*/

    }

    void Update()
    {

        if(Player)
        {
            
            cameraObject.transform.position = new Vector3(activePlayer.transform.position.x, activePlayer.transform.position.y, -10);

        }

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CursorLight.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);

        if(Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void PlayerDied(DeathType deathType = DeathType.Normal)
    {

        Text deathMSGTXT = DeathMessageObject.GetComponent<Text>();

        switch (deathType)
        {
            
            case DeathType.Normal:
            Instantiate(NormalDeath, Player.transform.position, Quaternion.identity);
            deathMSGTXT.text = NormalDeathMSG;
            break;

            case DeathType.Acid:
            Instantiate(AcidDeath, Player.transform.position, Quaternion.identity);
            deathMSGTXT.text = AcidDeathMSG;
            break;

        }

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
