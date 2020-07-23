using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.IO;
using System.Linq;

using TouchControlsKit;
using TMPro;

public class SurveyScript : MonoBehaviour
{
    private static int current = 0;
    private static int previous = 0;

    private static string[] answers = {"0", "0", "0", "0", "0", "0", "0", "0"};
    public string[] currentAnswers;

    // Start is called before the first frame update
    void Start()
    {
        currentAnswers = new string[8];
        currentAnswers = answers;

        foreach(string s in answers)
            Debug.Log(s);

        previous = current;
    }

    // Update is called once per frame
    void Update()
    {
        if (TCKInput.GetAction("Answer1", EActionEvent.Click))
        {
            Debug.Log("Answer1");
            answers[current++] = "1";
        }
        if (TCKInput.GetAction("Answer2", EActionEvent.Click))
        {
            Debug.Log("Answer2");
            answers[current++] = "2";
        }
        if (TCKInput.GetAction("Answer3", EActionEvent.Click))
        {
            Debug.Log("Answer3");
            answers[current++] = "3";
        }
        if (TCKInput.GetAction("Answer4", EActionEvent.Click))
        {
            Debug.Log("Answer4");
            answers[current++] = "4";
        }
        if (TCKInput.GetAction("Answer5", EActionEvent.Click))
        {
            Debug.Log("Answer5");
            answers[current++] = "5";
        }

        if (TCKInput.GetAction("AnswerOK", EActionEvent.Click))
        {
            TMP_Dropdown dd1 = GameObject.Find("Dropdown1").GetComponent<TMP_Dropdown>();
            TMP_Dropdown dd2 = GameObject.Find("Dropdown2").GetComponent<TMP_Dropdown>();
            TMP_Dropdown dd3 = GameObject.Find("Dropdown3").GetComponent<TMP_Dropdown>();

            if(dd1.value != 0 && dd2.value != 0 && dd3.value != 0)
            {
                Debug.Log("Answer5 - " + dd1.value.ToString() + "," + dd2.value.ToString() + "," + dd3.value.ToString());
                answers[current++] = dd1.value.ToString() + "," + dd2.value.ToString() + "," + dd3.value.ToString();
            }
        }

        if(current+1 >= 9)
        {
            Debug.Log("Survey finished");
            string id = SystemInfo.deviceUniqueIdentifier;
            StreamWriter wr = new StreamWriter(Application.persistentDataPath + "/" + id + "/" + "SURVEY_ANSWERS.txt", false);
            foreach(string ans in answers)
            {
                wr.Write(ans);
                    wr.Write("\n");
            }
            wr.Close();
            SceneManager.LoadScene("Upload", LoadSceneMode.Single);
        }

        if(previous != current)
            SceneManager.LoadScene("Survey" + (current+1).ToString(), LoadSceneMode.Single);
    }
}
