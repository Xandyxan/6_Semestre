using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class OpenDoorTest : MonoBehaviour
{
    [Header("Door Opening Camera Shake")]
    [SerializeField] CinemachineVirtualCamera camToShake;
    [Space]
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
        CinemachineShake.Instance.ShakeCamera(.3f, 1f, 1.7f, camToShake);
        Destroy(this);
    }
}
