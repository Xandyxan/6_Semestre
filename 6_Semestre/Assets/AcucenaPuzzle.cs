using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class AcucenaPuzzle : MonoBehaviour, ISelectable, IInteractable
{
    [Header("Puzzle Cameras")]
    [SerializeField] private Cinemachine.CinemachineVirtualCamera puzzleCamera;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera acucenaCamera;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera keyCamera;

    [Header("Selection")]
    [Tooltip("The visual feedback for an item that can be interacted with")]
    [SerializeField] private GameObject interactionFeedback;

    [Header("Puzzle UI")]
    [SerializeField] private GameObject smallInventory;

    [Header("Flowers Array")]
    [SerializeField] private GameObject[] flowers;

    [Header("Others")]
    [SerializeField] private DialogueManager2 dialogueManager;
    [SerializeField] private GameObject acucenaCharacter;
    [SerializeField] private GameObject key;

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

    private IEnumerator FinishPuzzle()
    {
        isCompleted = true;
        StopInteract();
        acucenaCharacter.SetActive(true);
        acucenaCamera.gameObject.SetActive(true);
        acucenaCamera.Priority = 15;
        key.SetActive(true);
        Invoke("ExecuteDialogue", 2f);

        yield return new WaitForSeconds(8f);
        keyCamera.gameObject.SetActive(true);
        keyCamera.Priority = 16;

        yield return new WaitForSeconds(3f);
        acucenaCamera.gameObject.SetActive(false);
        keyCamera.gameObject.SetActive(false);
        acucenaCharacter.SetActive(false);
        isCompleted = true;
    }

    public void ActivateFlowers(int i)      //dalia = 0, violeta = 1, rosa = 2;
    {
        flowers[i].SetActive(true);
    }

    public void CheckFlowers()
    {
        if (!isCompleted)
        {
            int count = 0;

            for (int i = 0; i < flowers.Length; i++)
            {
                if (flowers[i].activeSelf)
                {
                    count++;
                }
            }

            if (count == flowers.Length)
            {
                //Debug.LogError("Todas as flores estão ai");
                StartCoroutine(FinishPuzzle());
            }
        }
    }

    public void ExecuteDialogue()
    {
        dialogueManager.ExecuteDialogue(4);
    }
}
