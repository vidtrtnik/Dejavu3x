using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour
{
    public int playthrough = 1;

    public bool key1 = false;
    public bool key2 = false;
    public bool key3 = false;

    public bool waitForRespawn = false;

    public int[] controllersOrder = { -1, -1, -1 };
    public int currentControllerNum = -1;

    Respawn respawn;

    public GameObject enemy;
    private GameObject game;

    public void PlayerGotKey()
    {
        if (respawn.respawnRequest && respawn.newPlaythrough)
        {
            waitForRespawn = true;
            return;
        }
        else
            waitForRespawn = false;

        game.GetComponent<AssignController>().AssignNewController();

        playthrough++;
        if (playthrough <= 3)
        {
            GameObject nextKey = GameObject.Find("Key" + playthrough.ToString());

            Vector3 nextKeyPos = nextKey.transform.position;
            nextKeyPos.x -= 20.0f;
            nextKey.transform.position = nextKeyPos;

            GameObject enemyClone = (GameObject) Instantiate(enemy, enemy.transform);
            enemyClone.name = enemy.name;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        respawn = GetComponent<Respawn>();
        game = GameObject.Find("Game");

        GameObject.Find("FireI1").GetComponent<ParticleSystem>().Stop();
        GameObject.Find("FireI2").GetComponent<ParticleSystem>().Stop();
        GameObject.Find("FireI3").GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (waitForRespawn)
            PlayerGotKey();
    }
}
