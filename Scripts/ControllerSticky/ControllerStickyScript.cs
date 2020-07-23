using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerStickyScript : MonoBehaviour
{
    private GameObject telemetry;
    private InputStatistics inputStatistics;

    public bool left = false;
    public bool right = false;
    public bool attack = false;
    public bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        telemetry = GameObject.Find("Telemetry");
        inputStatistics = telemetry.GetComponent<InputStatistics>();
    }

    // Update is called once per frame
    void Update()
    {
        List<Touch> touchesBegan = inputStatistics.GetTouchesBegan();

        foreach( Touch touch in touchesBegan)
        {
            string hit = inputStatistics.evaluateTouch2(touch.position);
            if (hit == "ButtonRight")
            {
                //Debug.Log("RIGHT!");
                if (right)
                    right = false;
                else
                    if(!left)
                        right = true;

                left = false;

            }
            if (hit == "ButtonLeft")
            {
                //Debug.Log("LEFT!");
                if (left)
                    left = false;
                else
                    if(!right)
                        left = true;
                        
                right = false;
            }

            /*if (hit == "ButtonJump")
            {
                jump = true;
            }
            if (hit == "ButtonAttack")
            {
                attack = true;
            }
            */
        }
    }
}
