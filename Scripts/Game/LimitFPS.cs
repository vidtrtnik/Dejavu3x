using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitFPS : MonoBehaviour
{
    public bool limitFPS = true;
    public int targetFPS = 30;

     void Start() 
     {
         QualitySettings.vSyncCount = 2;
         Application.targetFrameRate = -1;
     }
}
