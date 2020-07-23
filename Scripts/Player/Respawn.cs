using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject beginning;

    public bool respawnRequest = false;
    public Vector3 respawnLocation;

    public bool newPlaythrough = false;

    private Logger logger;
    private Death death;
    private SFXScript sfx;
    private int deathCount;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("PlayerDeathParticles").GetComponent<ParticleSystem>().Stop();
        death = GetComponent<Death>();

        logger = GameObject.Find("Telemetry").GetComponent<Logger>();
        sfx = GameObject.Find("SFX").GetComponent<SFXScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (respawnRequest)
            if (!death.playingAnimation)
                RespawnAt(respawnLocation);

    }

    public void RespawnAt(Vector3 position)
    {
        GetComponent<CharacterController>().enabled = false;
        transform.position = position;
        GetComponent<CharacterController>().enabled = true;
        
        GameObject.Find("PlayerRestartExplosion").GetComponent<ParticleSystem>().Play();
        GetComponent<Movement>().UnlockMovement();

        respawnRequest = false;
        GameObject.Find("PlayerDeathParticles").GetComponent<ParticleSystem>().Stop();

        if (newPlaythrough)
            newPlaythrough = false;
    }

    public void RespawnAtBeginning()
    {
        GetComponent<Movement>().LockMovement();

        gameObject.GetComponent<Animator>().Play("AnimationPlayerGotKey");
        death.playing = 0;
        death.playingAnimation = true;

        newPlaythrough = true;
        respawnRequest = true;
        respawnLocation = beginning.transform.position;

        GameObject.Find("PlayerRestartParticles").GetComponent<ParticleSystem>().Play();
    }

    public void RespawnAtCheckpoint(Collider collider)
    {
        deathCount++;
        logger.LogPlayerDeath(transform.position, collider.gameObject.name, deathCount, GameObject.Find("Game").GetComponent<AssignController>().controllerNum);

        sfx.playPlayerDeath();

        GetComponent<Movement>().LockMovement();

        TrapScript trap = collider.gameObject.GetComponent<TrapScript>();
        GameObject checkpoint = trap.checkpoint;

        death.PlayAnimation();
        GameObject.Find("PlayerDeathParticles").GetComponent<ParticleSystem>().Play();
        respawnRequest = true;
        respawnLocation = checkpoint.transform.position;

        Handheld.Vibrate();
    }
}
