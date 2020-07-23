using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    Animator animator;
    public bool playingAnimation = false;
    public float playing = 0.0f;

    public float duration = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playing += Time.fixedDeltaTime;

        if (playingAnimation)
            if (playing > duration)
                playingAnimation = false;
    }

    public void PlayAnimation()
    {
        //Debug.Log("Playing animation...");

        // Variable 'playing' is keeping track of running animation
        playing = 0.0f;
        playingAnimation = true;

        animator.Play("AnimationPlayerDeath");
    }
}
