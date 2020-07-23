using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TouchControlsKit;

public class DisclaimerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TCKInput.GetAction("ButtonDummy", EActionEvent.Click))
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
