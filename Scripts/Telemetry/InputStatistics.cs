using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class InputStatistics : MonoBehaviour
{
    public GameObject touchVisualization;
    public GameObject touchVisualizationText;

    List<Touch> touchesPR = new List<Touch>();
    List<Touch> touchesBeganPR = new List<Touch>();
    List<Touch> touchesMovedPR = new List<Touch>();
    List<Touch> touchesStationaryPR = new List<Touch>();
    List<Touch> touchesEndedPR = new List<Touch>();

    List<Touch> touchesHITS = new List<Touch>();
    List<Touch> touchesMISSES = new List<Touch>();

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    TelemetryDebugInfo telemetryDebugInfo; 
    Logger logger;

    // Start is called before the first frame update
    void Start()
    {
        telemetryDebugInfo = GetComponent<TelemetryDebugInfo>();

        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GameObject.Find("_TCKCanvas").GetComponent<GraphicRaycaster>();
        //Debug.Log(m_Raycaster);
        //Fetch the Event System from the Scene
        m_EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        //Debug.Log(m_EventSystem);

        logger = GetComponent<Logger>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Raycaster = GameObject.Find("_TCKCanvas").GetComponent<GraphicRaycaster>();

        List<Touch> touches = new List<Touch>();
        touches.AddRange(Input.touches);

        List<Touch> touchesBegan = new List<Touch>();
        List<Touch> touchesMoved = new List<Touch>();
        List<Touch> touchesStationary = new List<Touch>();
        List<Touch> touchesEnded = new List<Touch>();

        if (touches.Count > 0)
        {
            //Debug.Log("Previous touches: " + touchesBeganPR.Count + ", " + touchesEndedPR.Count + ", " + touchesMovedPR.Count + ", " + touchesStationaryPR.Count);
            //Debug.Log("Touches count: " + touches.Count);
            //Debug.Log("H: " + touchesHITS.Count);
            //Debug.Log("M: " + touchesMISSES.Count);

            foreach (Touch touch in touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        //visualizeTouch(touch);
                        touchesBegan.Add(touch);
                        //Debug.Log("Evaluated: " + evaluateTouch2(touch.position));
                        telemetryDebugInfo.PrintTouchInfo(touch, evaluateTouch(touch.position));

                        string target = evaluateTouch2(touch.position);
                        logger.LogFTouch(touch, target, logger.GetTime());

                        if (!evaluateTouch(touch.position).Equals(""))
                            touchesHITS.Add(touch);
                        else
                            touchesMISSES.Add(touch);
                        break;

                    case TouchPhase.Moved:
                        touchesMoved.Add(touch);
                        break;

                    case TouchPhase.Stationary:
                        touchesStationary.Add(touch);
                        break;

                    case TouchPhase.Ended:
                        touchesEnded.Add(touch);
                        break;

                    default:
                        break;
                }
            }
        }

        touchesPR.Clear();
        touchesBeganPR.Clear();
        touchesEndedPR.Clear();
        touchesMovedPR.Clear();
        touchesStationaryPR.Clear();

        touchesPR.AddRange(touches);
        touchesBeganPR.AddRange(touchesBegan);
        touchesEndedPR.AddRange(touchesEnded);
        touchesMovedPR.AddRange(touchesMoved);
        touchesStationaryPR.AddRange(touchesStationary);
    }


    // Function evaluateTouch returns name of controller button, which is hit by touch
    public string evaluateTouch(Vector3 touchPosition)
    {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = touchPosition;

        List<RaycastResult> results = new List<RaycastResult>();

        m_Raycaster.Raycast(m_PointerEventData, results);
        foreach (RaycastResult result in results)
        {
            string name = result.gameObject.name;
            //Debug.Log(name);
            if (name == "ButtonJump" || name == "ButtonAttack" || name == "ButtonLeft" || name == "ButtonRight")
                return name;
        }

        return string.Empty;
    }

    // Function evaluateTouch2 always returns name of object which is hit by touch
    public string evaluateTouch2(Vector3 touchPosition)
    {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = touchPosition;

        List<RaycastResult> results = new List<RaycastResult>();

        m_Raycaster.Raycast(m_PointerEventData, results);
        foreach (RaycastResult result in results)
        {
            string name = result.gameObject.name;
            return name;
        }

        return string.Empty;
    }

    public void visualizeTouch(Touch touch)
    {
        GameObject canvas = GameObject.Find("VisualizationCanvas");
        Camera mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();

        float Z = mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, mainCamera.nearClipPlane)).z;

        Vector3 uiPoint = touch.position;
        //uiPoint.x = uiPoint.x - uiCamera.pixelWidth / 2;
        //uiPoint.y = uiPoint.y - uiCamera.pixelHeight / 2;

        // Calculate correct coordinates for object instantiation
        uiPoint.x = uiPoint.x - canvas.GetComponent<RectTransform>().rect.width / 2;
        uiPoint.y = uiPoint.y - canvas.GetComponent<RectTransform>().rect.height / 2;
        uiPoint.z = Z;

        GameObject visualization = Instantiate(touchVisualization, uiPoint, Quaternion.identity);
        visualization.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.height / 10, Screen.height / 10);
        visualization.transform.SetParent(canvas.transform, false);
        visualization.transform.SetSiblingIndex(0);
        
        GameObject visualizationText = Instantiate(touchVisualizationText, new Vector3(uiPoint.x-35.0f, uiPoint.y+55.0f, uiPoint.z), Quaternion.identity);
        visualizationText.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width * 2 / 10, Screen.height / 10);
        visualizationText.transform.SetParent(canvas.transform, false);
        visualizationText.transform.SetSiblingIndex(0);
        visualizationText.GetComponent<TouchVisualizationTextScript>().SetInitial(touch.position.x, touch.position.y, touch.fingerId);
    }

    public List<Touch> GetTouchesHITS()
    {
        return touchesHITS;
    }

    public List<Touch> GetTouchesMISSES()
    {
        return touchesMISSES;
    }

    public List<Touch> GetTouchesBegan()
    {
        return touchesBeganPR;
    }
}
