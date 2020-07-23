using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TelemetryDebugInfo : MonoBehaviour
{
    UnityEngine.UI.Text debugText;
    int debugTextNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetDebugText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PrintTouchInfo(Touch touch, string hitName)
    {
        // Only allow 15 lines of debug information
        if (debugTextNum > 15)
        {
            debugTextNum = 0;
            debugText.text = "Touches:";
        }

        debugText.text += "\n" + touch.position.x + "," + touch.position.y + " - " + hitName + " - " + touch.pressure + "," + touch.radius + " : " + Time.time.ToString();
        debugTextNum++;
    }

    public void GetDebugText()
    {
        debugText = GameObject.Find("TelemetryDebugText").GetComponent<UnityEngine.UI.Text>();
        debugText.text = "Touches:";
    }
}
