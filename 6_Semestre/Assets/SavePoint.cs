using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour, ISelectable, IInteractable
{
    [Header("Cameras")]
    [SerializeField] private Cinemachine.CinemachineVirtualCamera statueCamera;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera padreCamera;

    [Header("Selection")]
    [Tooltip("The visual feedback for an item that can be interacted with")]
    [SerializeField] private GameObject interactionFeedback;

    [Header("Others")]
    [SerializeField] private DialogueManager2 dialogueManager;
    [SerializeField] private int dialogueIndex;
    [SerializeField] private GameObject padre;
    [SerializeField] private GameObject feedBack;

    private bool alreadyInterected;
    private bool isInteracting;

    public void Deselect()
    {
        interactionFeedback.SetActive(false);
    }

    public void Interact()
    {
        StartCoroutine(FocusBody());
    }

    public void Select()
    {
        interactionFeedback.SetActive(true);
    }

    private void Awake()
    {
        
    }

    private IEnumerator FocusBody()
    {
        isInteracting = true;

        if (!alreadyInterected)
        {   
            statueCamera.gameObject.SetActive(true);
            statueCamera.Priority = 15;
            feedBack.SetActive(true);
            
            yield return new WaitForSeconds(4f);
            if(dialogueManager) dialogueManager.ExecuteDialogue(dialogueIndex);
            if(padre) padre.SetActive(true);

            alreadyInterected = true;
            statueCamera.gameObject.SetActive(false);
            padreCamera.gameObject.SetActive(true);
            padreCamera.Priority = 15;
            isInteracting = false;
        }
        else
        {
            statueCamera.gameObject.SetActive(true);
            statueCamera.Priority = 15;

            yield return new WaitForSeconds(4f);
            statueCamera.gameObject.SetActive(false);
        }


    }
}
