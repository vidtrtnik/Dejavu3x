using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorCheck : MonoBehaviour
{
    public GameObject sparks;
    GameObject player;
    Respawn respawn;
    SFXScript sfx;

    Logger logger;

    int playerKills = 0;
    int enemyKills = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        respawn = player.GetComponent<Respawn>();
        logger = GameObject.Find("Telemetry").GetComponent<Logger>();
        sfx = GameObject.Find("SFX").GetComponent<SFXScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject gameObject = collision.collider.gameObject;

        // Enemy has hit the Player with a sword
        if (gameObject.name == "EnemySword" && tag == "Player")
        {
            enemyKills++;
            sfx.playSwordClash1();
            Handheld.Vibrate();
            //logger.LogPlayerDeath(transform.position, "Enemy", enemyKills, GameObject.Find("Game").GetComponent<AssignController>().controllerNum);

            foreach (ContactPoint contact in collision.contacts)
                Instantiate(sparks, contact.point, sparks.transform.rotation);

            GameObject enemy = GameObject.Find("Enemy");
            enemy.GetComponent<EnemyScript>().backOff = true;
            respawn.RespawnAtCheckpoint(collision.collider);
        }

        // Player has hit the Enemy with a sword
        if (gameObject.name == "PlayerSword" && tag == "Enemy")
        {
            GameObject player = GameObject.Find("Player");

            if (player.GetComponent<Movement>().attack)
            {
                playerKills++;
                sfx.playSwordClash2();
                logger.LogPlayerKill(player.transform.position, GameObject.Find("Game").GetComponent<AssignController>().controllerNum);

                foreach (ContactPoint contact in collision.contacts)
                    Instantiate(sparks, contact.point, sparks.transform.rotation);

                GameObject enemy = GameObject.Find("Enemy");
                Destroy(enemy);
            }
        }
    }
}
