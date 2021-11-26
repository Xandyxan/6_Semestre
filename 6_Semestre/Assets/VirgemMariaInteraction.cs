using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class VirgemMariaInteraction : MonoBehaviour, ISelectable, IInteractable
{
    [Header("Puzzle Cameras")]
    [SerializeField] private Cinemachine.CinemachineVirtualCamera puzzleCamera;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera doorCamera;

    [Header("Selection")]
    [Tooltip("The visual feedback for an item that can be interacted with")]
    [SerializeField] private GameObject interactionFeedback;

    [Header("Puzzle UI")]
    [SerializeField] private GameObject smallInventory;

    [Header("Others")]
    //[SerializeField] private DialogueManager2 dialogueManager;
    [SerializeField] private GameObject heartObject;
    [SerializeField] private MoveUp door;

    private bool isInteracting;
    private bool isCompleted;

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
            if (!isCompleted)
            {
                StartInteract();
            }

        }
        else
        {
            StopInteract();
        }
    }

    private void Awake()
    {
        isInteracting = false;
        isCompleted = false;
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
        GameManager.instance.puzzleNumber = 2;
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

    private IEnumerator FinishPuzzle()
    {
        isCompleted = true;
        heartObject.SetActive(true);

        yield return new WaitForSeconds(2f);
        StopInteract();
        doorCamera.gameObject.SetActive(true);
        doorCamera.Priority = 15;
        door.RunCoroutine();

        yield return new WaitForSeconds(3f);
        doorCamera.gameObject.SetActive(false);
    }

    public void RunCoroutine()
    {
        StartCoroutine(FinishPuzzle());
    }
}
