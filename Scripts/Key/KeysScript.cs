using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysScript : MonoBehaviour
{
    private float rotationSpeed = 45.0f;
    private float amplitude = 0.1f;
    private float frequency = 3.0f;

    float initialY = 0.0f;
    Vector3 position = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        initialY = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0.0f, Time.fixedDeltaTime * rotationSpeed, 0.0f), Space.World);
        
        position = transform.position;
        position.y = initialY + Mathf.Sin(frequency * Time.time) * amplitude;

        transform.position = position;
    }
}
