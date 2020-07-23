using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitResolution : MonoBehaviour
{
    public bool limitResolution = false;
    public bool scaleResolution = false;
    public int resolution_x;
    public int resolution_y;
    public float scale_coeff;

    // Start is called before the first frame update
    void Start()
    {
        if(!limitResolution)
        {
            resolution_x = Screen.currentResolution.width;
            resolution_y = Screen.currentResolution.height;
        }

        if(!scaleResolution)
        {
            scale_coeff = 1.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(limitResolution)
        {
            Screen.SetResolution(resolution_x, resolution_y, true);
            limitResolution = false;
        }

        if(scaleResolution)
        {
            Screen.SetResolution((int)(resolution_x * scale_coeff), (int)(resolution_y * scale_coeff), true);
            scaleResolution = false;
        }
    }
}
