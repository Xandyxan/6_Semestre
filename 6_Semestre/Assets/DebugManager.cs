using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public GameObject saveDebugUI;

    private void Start()
    {
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (saveDebugUI != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if(saveDebugUI.activeSelf) saveDebugUI.SetActive(false);
                else saveDebugUI.SetActive(true);
            }
        }
        else
        {
            Debug.Log("There is not a Save Debug selected");
        }
        #endif   
    }
}
