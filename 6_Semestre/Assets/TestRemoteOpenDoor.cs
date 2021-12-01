using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRemoteOpenDoor : MonoBehaviour
{
    public Doors doortest;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            doortest.Interact();
        }        
    }
}
