using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DebugInfo : MonoBehaviour
{
    public bool debugInfo = true;

    GameObject gameDebugText;
    UnityEngine.UI.Text debugText;


    float deltaTime;

    string device;
    string os;
    string cpu;
    string ram;
    string gpu;
    string gtype;
    string gpumem;
    Resolution resolution;
    Logger logger;

    PerformanceCounter cpuCounter;
    PerformanceCounter ramCounter;

    // Start is called before the first frame update
    void Start()
    {
        device = SystemInfo.deviceModel;
        os = SystemInfo.operatingSystem;
        
        cpu = SystemInfo.processorType;
        ram = SystemInfo.graphicsMemorySize.ToString();

        gpu = SystemInfo.graphicsDeviceName;
        gtype = SystemInfo.graphicsDeviceType.ToString();
        gpumem = SystemInfo.graphicsMemorySize.ToString();

        resolution = Screen.currentResolution;

        cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        ramCounter = new PerformanceCounter("Memory", "Available MBytes");

        logger = GameObject.Find("Telemetry").GetComponent<Logger>();
        logger.WriteDeviceInfo(device, os, cpu, ram, gpu, gtype, resolution.width.ToString(), resolution.height.ToString());

        GetDebugText();
    }

    // Update is called once per frame
    void Update()
    {
        if (!debugInfo)
        {
            gameDebugText.SetActive(false);
            return;
        }

        //cpuCounter.NextValue();
        //ramCounter.NextValue();
        //System.Threading.Thread.Sleep(1000);

        GetDebugText();
        gameDebugText.SetActive(true);

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        float ft = (1.0f / fps) * 1000;

        debugText.text = "Info:" + "\n";
        //debugText.text += "ID:\t" + SystemInfo.deviceUniqueIdentifier + "\n";
        debugText.text += "ID:\t" + "null" + "\n";
        debugText.text += "Device model:\t" + device + "\n";
        debugText.text += "OS:\t" + os + "\n";
        debugText.text += "Processor:\t" + cpu + "\n";
        debugText.text += "RAM:\t" + ram + "\n";
        debugText.text += "Graphics:\t" + gpu + "\n";
        debugText.text += "API:\t" + gtype + "\n";
        debugText.text += "Video memory:\t" + gpumem + "\n";
        debugText.text += "Resolution: " + resolution.width + ", " + resolution.height + " @ " + resolution.refreshRate + "Hz \n";

        //debugText.text += "\n\n";

        //debugText.text += "CPU Usage: " + cpuCounter.NextValue() + " %" + "\n";
        //debugText.text += "Available RAM: " + ramCounter.NextValue() + " MB" + "\n";

        debugText.text += "\n\n";

        debugText.text += "FPS: " + fps + "\n";
        debugText.text += "FT: " + ft + "\n";
    }

    public void GetDebugText()
    {
        gameDebugText = GameObject.Find("GameDebugText");
        debugText = gameDebugText.GetComponent<UnityEngine.UI.Text>();
        gameDebugText.SetActive(true);
        debugText.text = "Info:";
    }
}
