using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXScript : MonoBehaviour
{
    public AudioSource buttonClick;
    public AudioSource playerDeath;
    public AudioSource swordClash1;
    public AudioSource swordClash2;
    public AudioSource swordClash3;
    public AudioSource respawnSound;
    public AudioSource keySound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playButtonClick()
    {
        buttonClick.Play();
    }

    public void playPlayerDeath()
    {
        playerDeath.Play();
    }

    public void playSwordClash1()
    {
        swordClash1.Play();
    }

    public void playSwordClash2()
    {
        swordClash2.Play();
    }

    public void playSwordClash3()
    {
        swordClash3.Play();
    }

    public void playRespawnSound()
    {
        respawnSound.Play();
    }

    public void playKeySound()
    {
        keySound.Play();
    }
}
