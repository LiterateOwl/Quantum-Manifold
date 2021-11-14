using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiButton : MonoBehaviour
{
    public bool button;
    public float buttonStaysPressedTime;

    private float buttonPressTime;

    private void Start()
    {
        buttonPressTime = -1.0f;
    }

    private void Update()
    {
        if(button)
        {
            if (buttonPressTime < 0.0f) buttonPressTime = Time.time;
            else if(Time.time - buttonPressTime > buttonStaysPressedTime)
            {
                buttonPressTime = -1.0f;
                button = false;
            }
        }
    }
}
