using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckTriggers : MonoBehaviour
{
    GameObject player;
    IsGrounded isGrounded;
    Respawn respawn;
    Progress progress;

    Logger logger;
    SFXScript sfx;

    private GameObject hud;

    private int deathCount = 0;
    private int keys = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        isGrounded = GetComponent<IsGrounded>();
        respawn = player.GetComponent<Respawn>();
        progress = player.GetComponent<Progress>();

        logger = GameObject.Find("Telemetry").GetComponent<Logger>();
        sfx = GameObject.Find("SFX").GetComponent<SFXScript>();

        hud = GameObject.Find("HUD");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Trap") && !respawn.respawnRequest)
        {
            deathCount++;

            //Debug.Log("IsGrounded - OnTriggerEnter() TRAP!: " + collider.gameObject.name);
            respawn.RespawnAtCheckpoint(collider);
        }

        if (collider.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("IsGrounded - OnTriggerEnter(): " + collider.gameObject.name);
            isGrounded.increaseGroundCollisions();
        }

        if (collider.gameObject.CompareTag("Key"))
        {
            hud.GetComponent<TextMeshProUGUI>().text = "Keys: " + (++keys).ToString() + " / 3";
            //Debug.Log("IsGrounded - OnTriggerEnter() KEY!: " + collider.gameObject.name);

            GameObject.Find("FireI" + (progress.playthrough).ToString()).GetComponent<ParticleSystem>().Play();

            Destroy(collider.gameObject);
            logger.LogPlayerGK(player.transform.position, GameObject.Find("Game").GetComponent<AssignController>().controllerNum);

            sfx.playKeySound();

            if (progress.playthrough < 3)
                respawn.RespawnAtBeginning();

            if (respawn.newPlaythrough && progress.playthrough < 3)
                progress.PlayerGotKey();
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Trap"))
        {
            //Debug.Log("IsGrounded - OnTriggerExit() TRAP!:" + collider.gameObject.name);
        }

        if (collider.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("IsGrounded - OnTriggerExit(): " + collider.gameObject.name);
            isGrounded.decreaseGroundCollisions();
        }
    }


}
