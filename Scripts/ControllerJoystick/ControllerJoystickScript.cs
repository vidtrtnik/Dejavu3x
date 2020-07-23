using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerJoystickScript : MonoBehaviour
{
    private GameObject ButtonJump;
    private GameObject ButtonAttack;

    private float lastTime = 0.0f;
    public float idleTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        ButtonJump = GameObject.FindWithTag("ButtonJumpJoystick");
        ButtonAttack = GameObject.FindWithTag("ButtonAttackJoystick");
    }

    // Update is called once per frame
    void Update()
    {
        idleTime = Time.time - lastTime;
        if(idleTime > 1.5f)
        {
            Color ButtonJumpColor = ButtonJump.GetComponent<Image>().color;
            Color ButtonAttackColor = ButtonAttack.GetComponent<Image>().color;

            if(ButtonJumpColor.a > 0.35f)
            {
                ButtonJumpColor.a -= 0.02f;
                ButtonJump.GetComponent<Image>().color = ButtonJumpColor;
            }

            if(ButtonAttackColor.a > 0.35f)
            {
                ButtonAttackColor.a -= 0.02f;
                ButtonAttack.GetComponent<Image>().color = ButtonAttackColor;
            }
        }
    }

    public void RestoreVisibility()
    {
        lastTime = Time.time;

        Color ButtonJumpColor = ButtonJump.GetComponent<Image>().color;
        Color ButtonAttackColor = ButtonAttack.GetComponent<Image>().color;

        ButtonJumpColor.a = 0.78f;
        ButtonAttackColor.a = 0.78f;

        ButtonJump.GetComponent<Image>().color = ButtonJumpColor;
        ButtonAttack.GetComponent<Image>().color = ButtonAttackColor;
    }
}
