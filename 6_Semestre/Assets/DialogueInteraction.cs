using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : MonoBehaviour, IInteractable, ISelectable
{
    // Quando o player interagir com o icone, o foco será trocado para uma câmera de interação e um diálogo a respeito do item observado irá rolar.
    [Header("Camera")]
    [SerializeField] private GameObject player; 
    [SerializeField] private Cinemachine.CinemachineVirtualCamera interactionCam;
    private Camera mainCam;

    [Header("Selection")]
    [Tooltip("The visual feedback for an item that can be interacted with")]
    [SerializeField] private GameObject interactionFeedback;

    [Header("Dialogue")]
    [SerializeField] int _dialogueIndex;
    [SerializeField] DialogueManager2 dialogue;

    private bool firstInput = false;
    private bool isInteracting = false; // we will use this to disable player control, change to interaction camera and register correct input when interacting

    private void Awake()
    {
        mainCam = Camera.main;
    }

    public void Interact()
    {
        if (isInteracting) return;
        firstInput = true;
        isInteracting = true;

        interactionCam.Priority = 12;

        GameManager.instance.removePlayerControlEvent?.Invoke();
        HidePlayerLayer(); // dialogo será chamado aqui
      
        Invoke("SetFirstInputFalse", 0.1f);
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E) && !firstInput) { StopInteracting(); }
    }

    private void StopInteracting()
    {
        if (!isInteracting) return;
        isInteracting = false;

        GameManager.instance.returnPlayerControlEvent?.Invoke();
        ShowPlayerLayer();

        interactionCam.Priority = 5;
    }

    private void SetFirstInputFalse()
    {
        firstInput = false;
    }

    private void ShowPlayerLayer()
    {
        mainCam.cullingMask |= 1 << LayerMask.NameToLayer("Player");
    }

    private void HidePlayerLayer()
    {
        mainCam.cullingMask &= ~(1 << LayerMask.NameToLayer("Player"));
        dialogue.ExecuteDialogue(_dialogueIndex);
    }

    public void Select()
    {
        interactionFeedback.SetActive(true);
    }

    public void Deselect()
    {
        interactionFeedback.SetActive(false);
    }

}
