using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TouchControlsKit;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TCKInput.GetAction("ButtonStart", EActionEvent.Click))
            SceneManager.LoadScene("World", LoadSceneMode.Single);

        if (TCKInput.GetAction("ButtonCredits", EActionEvent.Click))
            SceneManager.LoadScene("Credits", LoadSceneMode.Single);

        if (TCKInput.GetAction("ButtonExit", EActionEvent.Click))
            Application.Quit();
    }

}
