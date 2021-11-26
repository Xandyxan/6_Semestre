using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectBody : MonoBehaviour, ISelectable, IInteractable
{
    [Header("Cameras")]
    [SerializeField] private Cinemachine.CinemachineVirtualCamera bodyCamera;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera padreCamera;

    [Header("Selection")]
    [Tooltip("The visual feedback for an item that can be interacted with")]
    [SerializeField] private GameObject interactionFeedback;

    [Header("Others")]
    [SerializeField] private DialogueManager2 dialogueManager;
    [SerializeField] private int dialogueIndex;
    [SerializeField] private GameObject padre;

    private bool alreadyInterected;
    private bool isInteracting;

    public void Deselect()
    {
        interactionFeedback.SetActive(false);
    }

    public void Interact()
    {
        if(!isInteracting)
        {
            StartCoroutine(FocusBody());
        }
    }

    public void Select()
    {
        interactionFeedback.SetActive(true);
    }

    private void Awake()
    {
        alreadyInterected = false;
    }

    private IEnumerator FocusBody()
    {
        isInteracting = true;

        if (!alreadyInterected)
        {
            dialogueManager.ExecuteDialogue(dialogueIndex);
            bodyCamera.gameObject.SetActive(true);
            bodyCamera.Priority = 15;
            padre.SetActive(true);
            yield return new WaitForSeconds(26f);
            
            alreadyInterected = true;
            bodyCamera.gameObject.SetActive(false);
            padreCamera.gameObject.SetActive(true);
            padreCamera.Priority = 15;
            isInteracting = false;
        }

        
    }
}
