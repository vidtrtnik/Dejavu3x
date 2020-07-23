using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GatesScript : MonoBehaviour
{
    private GameObject game;
    private GameObject player;
    private GameObject telemetry;
    private Logger logger;

    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.Find("Game");
        player = GameObject.Find("Player");
        telemetry = GameObject.Find("Telemetry");
        logger = telemetry.GetComponent<Logger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int CheckKeys()
    {
        return player.GetComponent<Progress>().playthrough;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if(CheckKeys() >= 3)
            {
                logger.WriteTouchesLog();
                SceneManager.LoadScene("TheEnd", LoadSceneMode.Single);
            }
        }
    }
}
