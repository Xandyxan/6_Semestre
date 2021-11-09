using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BetterLookInteraction : MonoBehaviour, IInteractable, ISelectable
{
    [SerializeField] private List<CinemachineVirtualCamera> objCams = new List<CinemachineVirtualCamera>();
    private int lookCameraIndex = 0;

    private bool isInteracting = false; // we will use this to disable player control, change to interaction camera and register correct input when interacting.
    private bool firstInput = true; // buffer pra evitar do objeto deselecionar no momento que usa o comando de selecionar;

    [SerializeField] private GameObject player; // we will temporally disable the player gameobject when using the inspection camera. Need to rework later!
    private Camera mainCam;

    [Header("Selection")]
    [Tooltip("The visual feedback for an item that can be interacted with")]
    [SerializeField] private GameObject interactionFeedback;

    private void Awake()
    {
        mainCam = Camera.main; // substituir por uma var global no futuro
    }
    public void Interact()
    {
        if (isInteracting) return;
        firstInput = true;
        isInteracting = true;

        GameManager.instance.removePlayerControlEvent?.Invoke();
        HidePlayerLayer();
        Deselect();
        ChoseAnother(0);

        Invoke("SetFirstInputFalse", 0.1f);
    }

    private void SetFirstInputFalse()
    {
        firstInput = false;
    }

    private void Update()
    {
        if (isInteracting)
        {
            if (Input.GetKeyDown(KeyCode.A)) { ChoseAnother(-1); }
            else if (Input.GetKeyDown(KeyCode.D)) { ChoseAnother(1); }
        }
    }

    private void ChoseAnother(int DirToChose) // dirToChose will be either (-1) or (+1), the selected index will change based on that number.
    {
        if (objCams.Count > 0)
        {
            lookCameraIndex += DirToChose;

            if (lookCameraIndex > objCams.Count - 1) { lookCameraIndex = 0; }
            else if (lookCameraIndex < 0) { lookCameraIndex = objCams.Count - 1; }

                for (int i = 0; i < objCams.Count; i++)
                {
                    objCams[i].Priority = 5;
                }
                objCams[lookCameraIndex].Priority = 15;
        }
    }
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E) && !firstInput) { StopInteracting(); }
    }

    private void StopInteracting()
    {
        if (!isInteracting) { return; }
        print("StoppedInteracting");
        foreach (CinemachineVirtualCamera cam in objCams)
        {
            cam.Priority = 5;
        }
        isInteracting = false;

        GameManager.instance.returnPlayerControlEvent?.Invoke();

        ShowPlayerLayer();
        Select();
    }

    public void Select()
    {
        interactionFeedback.SetActive(true);
    }

    public void Deselect()
    {
        interactionFeedback.SetActive(false);
    }

    private void ShowPlayerLayer()
    {
        //playerController.SetCanMove(true);
        mainCam.cullingMask |= 1 << LayerMask.NameToLayer("Player");

    }

    private void HidePlayerLayer()
    {
        mainCam.cullingMask &= ~(1 << LayerMask.NameToLayer("Player"));
        // playerController.SetCanMove(false);
    }
}
