using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcTalk : MonoBehaviour, IInteractable, ISelectable
{
    [SerializeField] private CinemachineVirtualCamera cameraCharacter;
    [SerializeField] private DialogueManager2 dialogueManager;
    [SerializeField] private int index;
    [SerializeField] private GameObject feedBack;

    private bool isInteracting;
    private bool alreadyInteracted;

    public void Deselect()
    {
        feedBack.SetActive(false);
    }

    public void Interact()
    {
        if(!isInteracting && !alreadyInteracted)
        {
            StartInteract();
        }
 
    }

    public void Select()
    {
        if(!alreadyInteracted)
            feedBack.SetActive(true);
    }

    private void StartInteract()
    {
        isInteracting = true;

        dialogueManager.ExecuteDialogue(index);
        cameraCharacter.gameObject.SetActive(true);
        cameraCharacter.Priority = 15;

        alreadyInteracted = true;
    }

    private void StopInteract()
    {
        cameraCharacter.gameObject.SetActive(false);
    }

    private void Awake()
    {
        
    }

    private void Update()
    {
        
    }
}
