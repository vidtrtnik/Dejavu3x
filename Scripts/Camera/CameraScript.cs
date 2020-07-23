using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float offset_x = 0.5f;
    public float offset_y = 1.2f;
    public float offset_z = -3.5f;

    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPosition = new Vector3(
            Player.transform.position.x + offset_x,
            Player.transform.position.y + offset_y,
            Player.transform.position.z + offset_z
            );

        transform.position = cameraPosition;
    }
}
