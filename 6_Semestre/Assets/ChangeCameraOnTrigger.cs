using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeCameraOnTrigger : MonoBehaviour
{
    private CinemachineBrain brain;
    [SerializeField] private CinemachineVirtualCamera thisAreaCam;
    public CinemachineVirtualCamera VirtualCamera;

    [Header("Others")]
    [SerializeField] private GameObject _object;
    private bool alreadyDone;
   
    private void Awake()
    {
        brain = Camera.main.GetComponent<CinemachineBrain>();
        alreadyDone = false;
    }
   
    IEnumerator Start()
    {
        yield return null;
        VirtualCamera = brain.ActiveVirtualCamera as CinemachineVirtualCamera; 
    }

    private void OnTriggerEnter(Collider other)  // testar fazer isso com um sistemas sem o cinemachine ClearShot
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("AHHAHAHAHAAHAHH MUAHAHAHA");

            StartCoroutine(UpdateActiveCam()); // lembrar de ajustar o trecho de UpdateActiveCam no script do player, para que a movimentação seja de acordo com isso.

            if(!alreadyDone && _object != null)
            {
                _object.SetActive(true);
                alreadyDone = true;
            }
        }
    }

    private IEnumerator UpdateActiveCam()
    {
        yield return null;
        VirtualCamera = brain.ActiveVirtualCamera as CinemachineVirtualCamera;

        VirtualCamera.Priority = 8;
        thisAreaCam.Priority = 12;
        yield return null;
        VirtualCamera.gameObject.SetActive(false);
        thisAreaCam.gameObject.SetActive(true);
    }

}
