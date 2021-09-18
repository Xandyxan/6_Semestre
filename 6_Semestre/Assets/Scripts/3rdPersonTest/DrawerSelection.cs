using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerSelection : MonoBehaviour, IInteractable, ISelectable
{
    #region sumary
    // for  this script, we need to be able to chose from a list of drawers to interact with one of them. For some polishing, we can add a simple arrow pointing to 
    // the selected drawer, giving more feedback to the player. To change the arrow position to match with the one from the selected drawer, we can simply have a pos list.
    #endregion

    [SerializeField] List<Doors> drawers = new List<Doors>();
    private List<Renderer> drawerRenderes = new List<Renderer>();
    private int selectionIndex = 0;
    private bool isInteracting = false; // we will use this to disable player control, change to interaction camera and register correct input when interacting.
    private bool firstInput = true; // buffer pra evitar do objeto deselecionar no momento que usa o comando de selecionar;

    [Header("Camera")]
    [SerializeField] private GameObject player; // we will temporally disable the player gameobject when using the inspection camera. Need to rework later!
    [SerializeField] private Cinemachine.CinemachineVirtualCamera drawerCam;
    private Camera mainCam;

    [Header("Selection")]
    [Tooltip("The visual feedback for an item that can be interacted with")]
    [SerializeField] private GameObject interactionFeedback;

    private void Awake()
    {
        foreach(Doors drawer in drawers)
        {
            Renderer drawerRend = drawer.transform.GetChild(0).GetComponent<Renderer>();
            drawerRenderes.Add(drawerRend);
        }
        
        mainCam = Camera.main;
    }
    private void Update()
    {
        if (isInteracting)
        {
            if (Input.GetKeyDown(KeyCode.W)) { ChoseAnother(-1); }
            else if (Input.GetKeyDown(KeyCode.S)) { ChoseAnother(1); }
            else if (Input.GetKeyDown(KeyCode.Space)) { OpenSelectedDrawer(); }
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
        foreach (Renderer rend in drawerRenderes)
        {
            rend.material.DisableKeyword("_EMISSION");
        }
        isInteracting = false;
        GameManager.instance.returnPlayerControlEvent?.Invoke();
        ShowPlayerLayer();
        drawerCam.Priority = 5;
    }

    private void ChoseAnother(int DirToChose) // dirToChose will be either (-1) or (+1), the selected index will change based on that number.
    {
        if(drawers.Count > 0)
        {
            selectionIndex += DirToChose;

            if(selectionIndex > drawers.Count - 1) { selectionIndex = 0; }
            else if(selectionIndex < 0) { selectionIndex = drawers.Count -1; }

            foreach(Renderer rend in drawerRenderes)
            {
              rend.material.DisableKeyword("_EMISSION");
            }
            drawerRenderes[selectionIndex].material.EnableKeyword("_EMISSION");
        }
    }

    private void OpenSelectedDrawer()
    {
        drawers[selectionIndex].Interact();
    }


    void IInteractable.Interact()
    {
        if (isInteracting) { return; }
        print("INteragiu");
        firstInput = true;
        isInteracting = true;
        drawerCam.Priority = 12;
        HidePlayerLayer();
        GameManager.instance.removePlayerControlEvent?.Invoke();
        // change to inspection camera
        // disable player movement
        ChoseAnother(0);
        Invoke("SetFirstInputFalse", 0.1f);
    }


    private void SetFirstInputFalse()
    {
        print("AAAAA");
        firstInput = false;
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
