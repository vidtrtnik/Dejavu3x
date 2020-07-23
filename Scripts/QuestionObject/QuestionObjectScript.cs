using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class QuestionObjectScript : MonoBehaviour
{
    public float rotationSpeed;

    private GameObject questionS1;
    private GameObject questionS2;

    public GameObject[] controllerInstructions;

    private GameObject Game;
    private Screenshot screenshot;

    // Start is called before the first frame update
    void Start()
    {
        questionS1 = GameObject.Find("QuestionS1");
        questionS2 = GameObject.Find("QuestionS2");

        Game = GameObject.Find("Game");
        screenshot = Game.GetComponent<Screenshot>();
    }

    // Update is called once per frame
    void Update()
    {
        questionS1.transform.Rotate(new Vector3(0.0f, Time.fixedDeltaTime * rotationSpeed, 0.0f), Space.World);
        questionS2.transform.Rotate(new Vector3(0.0f, Time.fixedDeltaTime * rotationSpeed, 0.0f), Space.World);
    }

    // If Player enters the Question mark zone, show the instructions 
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            showInstructions();
        }
    }

    // If Player leaves the Question mark zone, hide the instructions 
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            hideInstructions();
        }

        // Take screenshot of GUI 
        // screenshot.TakeScreenshotOnlyGUI();
    }

    public void hideInstructions()
    {
        controllerInstructions = GameObject.FindGameObjectsWithTag("Instructions");
        foreach ( GameObject obj in controllerInstructions)
        {
            //Debug.Log("disabling...");
            obj.GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }

    public void showInstructions()
    {
        controllerInstructions = GameObject.FindGameObjectsWithTag("Instructions");
        foreach ( GameObject obj in controllerInstructions)
        {
            //Debug.Log("showing...");
            obj.GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }
}
