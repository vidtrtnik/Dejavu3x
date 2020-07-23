using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AssignController : MonoBehaviour
{
    public int controllerNum = 0;
    public GameObject controller;
    public string controllerString = "";
    public List<int> assignedControllers;

    private GameObject controllerDPAD;
    private GameObject controllerJOYSTICK;
    private GameObject controllerSTICKY;

    // Start is called before the first frame update
    void Start()
    {
        controllerDPAD = GameObject.Find("Controller-DPAD");
        controllerJOYSTICK = GameObject.Find("Controller-JOYSTICK");
        controllerSTICKY = GameObject.Find("Controller-STICKY");

        AssignNewController();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Randomly assign new controller and return its id number
    // 1 - DPad, 2 - Joystick, 3 - Sticky Keys
    public int AssignNewController()
    {
        do
        {
            controllerNum = Random.Range(1, 3+1);
        }
        while (assignedControllers.Contains(controllerNum));

        assignedControllers.Add(controllerNum);

        controllerDPAD.SetActive(false);
        controllerJOYSTICK.SetActive(false);
        controllerSTICKY.SetActive(false);

        if (controllerNum == 1)
        {
            controller = controllerDPAD;
            controllerString = "DPad";
        }
        if (controllerNum == 2)
        {
            controller = controllerJOYSTICK;
            controllerString = "Joystick";
        }
        if (controllerNum == 3)
        {
            controller = controllerSTICKY;
            controllerString = "Sticky";
        }

        controller.SetActive(true);

        GetComponent<DebugInfo>().GetDebugText();
        GameObject.Find("Telemetry").GetComponent<TelemetryDebugInfo>().GetDebugText();

        //Debug.Log("Assigned controller: " + controllerNum + ", " + controller.name);

        return controllerNum;
    }
}
