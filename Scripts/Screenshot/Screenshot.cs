using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    public int width;
    public int height;

    private Camera camera;

    private Logger logger;

    // Start is called before the first frame update
    void Start()
    {
        width = Screen.width;
        height = Screen.height;

        camera = GameObject.Find("MainCamera").GetComponent<Camera>();
        logger = GameObject.Find("Telemetry").GetComponent<Logger>();
    }

    // Update is called once per frame
    void Update()
    {
        width = Screen.width;
        height = Screen.height;
    }

    void LateUpdate()
    {
        if(Input.GetKeyDown("w"))
        {
            TakeScreenshotNoGUI();
        }
        if (Input.GetKeyDown("e"))
        {
            TakeScreenshot();
        }
        if (Input.GetKeyDown("u"))
        {
            TakeScreenshotOnlyGUI();
        }
    }

    void TakeScreenshotNoGUI()
    {
        //Debug.Log("TAKING");
        string filename = logger.DirPath + "/" + "Dejavu3x_scrNoGUI_" + System.DateTime.UtcNow.ToString("yy-MM-dd-HH-mm-ss") + ".png";

        RenderTexture rt = new RenderTexture(width, height, 24);
        camera.targetTexture = rt;

        Texture2D screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
        camera.Render();
        RenderTexture.active = rt;
        screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        
        camera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        byte[] data = screenshot.EncodeToPNG();
        System.IO.File.WriteAllBytes(filename, data);
    }

    public void TakeScreenshotOnlyGUI()
    {
        //Debug.Log("TAKING");
        QuestionObjectScript questionObjectScript = GameObject.Find("QuestionObject").GetComponent<QuestionObjectScript>();
        logger = GameObject.Find("Telemetry").GetComponent<Logger>();
        questionObjectScript.hideInstructions();

        Camera uiCamera = GameObject.Find("tckUICamera").GetComponent<Camera>();
        string filename = logger.DirPath + "/" + "Dejavu3x_scrOnlyGUI_" + GameObject.Find("Game").GetComponent<AssignController>().controllerString + ".png";

        RenderTexture rt = new RenderTexture(width, height, 24);
        uiCamera.targetTexture = rt;

        Texture2D screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
        uiCamera.Render();
        RenderTexture.active = rt;
        screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        
        uiCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        byte[] data = screenshot.EncodeToPNG();
        System.IO.File.WriteAllBytes(filename, data);

        //questionObjectScript.showInstructions();
    }

    void TakeScreenshot()
    {
        string filename = "Dejavu3x_screenshot_" + System.DateTime.UtcNow.ToString("yy-MM-dd-HH-mm-ss") + ".png";
        ScreenCapture.CaptureScreenshot(logger.DirPath + "/" + filename);
    }
}
