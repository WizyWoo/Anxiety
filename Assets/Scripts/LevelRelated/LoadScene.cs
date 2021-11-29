using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    public bool UseTrigger;
    public int SceneToLoad;

    public void LoadSceneNum(int num) => SceneManager.LoadScene(num);

    private void OnTriggerEnter2D(Collider2D col)
    {

        if(UseTrigger)
            if(col.gameObject.tag == "Player")
                {

                    Checkpoint.CheckPointController.FinishedLevel();
                    LoadSceneNum(SceneToLoad);

                }

    }

}
