using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    public bool grounded = false;
    private int groundCollisions = 0;

    public void increaseGroundCollisions()
    {
        groundCollisions++;
    }

    public void decreaseGroundCollisions()
    {
        groundCollisions--;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("IsGrounded - Start()");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (groundCollisions > 0)
            grounded = true;
        else
            grounded = false;
    }
}
