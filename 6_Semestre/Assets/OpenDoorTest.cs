using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorTest : MonoBehaviour
{
    [Header("Temp Stuff")]
    [SerializeField] private Doors door;
    [SerializeField] private RotationMechanismBase rotationMechanism;
   
    void Start()
    {
        rotationMechanism.puzzleSolvedDelegate -= OpenDoor;
        rotationMechanism.puzzleSolvedDelegate += OpenDoor;
    }

  
    private void OpenDoor()
    {
        door.Interact();
        Destroy(this);
    }
}
