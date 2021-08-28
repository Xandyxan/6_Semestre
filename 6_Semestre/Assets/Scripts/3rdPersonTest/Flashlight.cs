using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    // 🐈 ~ press f to toggle lights on / off

    private Light flashLight;
    private bool lightsOn = false;

    private void Awake()
    {
        flashLight = GetComponent<Light>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ClickLights();
        }
    }

    private void ClickLights()
    {
        lightsOn = !lightsOn;

        if (lightsOn) { flashLight.enabled = true; } 
        else { flashLight.enabled = false; }
    }
}
