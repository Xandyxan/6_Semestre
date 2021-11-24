using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class AcucenaPuzzle : MonoBehaviour, ISelectable, IInteractable
{
    [Header("Puzzle Camera")]
    [SerializeField] private Cinemachine.CinemachineVirtualCamera puzzleCamera;

    [Header("Selection")]
    [Tooltip("The visual feedback for an item that can be interacted with")]
    [SerializeField] private GameObject interactionFeedback;

    [Header("Puzzle UI")]
    [SerializeField] private GameObject smallInventory;

    [Header("Flowers Array")]
    [SerializeField] private GameObject[] flowers;

    [SerializeField] private DialogueManager2 dialogueManager;

    private bool isInteracting;

    public void Deselect()
    {
        interactionFeedback.SetActive(false);
    }

    public void Select()
    {
        interactionFeedback.SetActive(true);
    }
    public void Interact()
    {
        if (!isInteracting)
        {
            StartInteract();
        }
        else
        {
            StopInteract();
        }
    }

    private void Awake()
    {
        isInteracting = false;
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void StartInteract()
    {
        puzzleCamera.gameObject.SetActive(true);
        puzzleCamera.Priority = 15;
        smallInventory.SetActive(true);
        isInteracting = true;

        GameManager.instance.SetLockCursor(false);
        GameManager.instance.isOnPuzzle = true;
        GameManager.instance.puzzleNumber = 0;
    }

    private void StopInteract()
    {
        puzzleCamera.gameObject.SetActive(false);
        puzzleCamera.Priority = 5;
        smallInventory.SetActive(false);
        isInteracting = false;

        GameManager.instance.SetLockCursor(true);
        GameManager.instance.isOnPuzzle = false;
        GameManager.instance.puzzleNumber = -1;
    }

    private void FinishPuzzle()
    {

    }

    public void ActivateFlowers(int i)      //dalia = 0, violeta = 1, rosa = 2;
    {
        flowers[i].SetActive(true);
    }

    public void CheckFlowers()
    {
        int count = 0;

        for(int i = 0; i < flowers.Length; i++)
        {
            if(flowers[i].activeSelf)
            {
                count++;
            }
        }

        if (count == flowers.Length)
        {
            //Debug.LogError("Todas as flores estão ai");
            dialogueManager.ExecuteDialogue(4);
            StopInteract();
        }
    }
}
