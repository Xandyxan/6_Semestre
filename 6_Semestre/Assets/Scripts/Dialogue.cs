using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private PlayerController3rdPerson playerController;

    [Header("General")]
    [SerializeField] private string characterName;
    [SerializeField] [TextArea(1, 5)] private string[] speechs;
    private PlaySound dialogueSound;

    [Tooltip("This dialogue will be displayed only once?")]
    [SerializeField] private bool onlyOnce;
    private bool alreadyExecuted;

    [Header("Has a continuation dialogue?")]
    [SerializeField] private GameObject nextDialogue;
    private Dialogue nextDialogueScript;

    [Header("Dialogue Box UI")]
    private DialogueManager2 dialogueManager;

    [Header("Restrict Character Movement?")]
    [SerializeField] private bool restrictCharMovement;

    [Header("Time to play extra sound")]
    [SerializeField] bool endSound;

    [SerializeField] private float _countProvisorio;

    public static bool isSomeDialogueRunning;

    [Header("Others")]
    [SerializeField] private CinemachineVirtualCamera cameraToActive;
    public GameObject cameraToDesactive;
    public ItemObject item;
    [SerializeField] private InventoryObject playerInventory;
    [SerializeField] private DoorInteract doorInteract;

    private void Awake()
    {
        dialogueManager = GetComponentInParent<DialogueManager2>();
        dialogueSound = GetComponent<PlaySound>();


        alreadyExecuted = false;

        if (nextDialogue != null) { nextDialogueScript = nextDialogue.GetComponent<Dialogue>(); }
    }

    private void Start()
    {

    }

    public IEnumerator Speech()
    {
        isSomeDialogueRunning = true;
        dialogueManager.GetCharacterNameUI().text = characterName;

        if (restrictCharMovement)
        {
            playerController.TurnPlayerControllerOff();
        }

        if (cameraToActive)
        {
            cameraToActive.gameObject.SetActive(true);
            cameraToActive.Priority = 15;
        }

        if (!alreadyExecuted)
        {
            if (dialogueSound != null)
            {
                dialogueSound.PlayOneShoot2D();
            }

            dialogueManager.GetDialogueBox().SetActive(true);

            _countProvisorio = _countProvisorio / speechs.Length;

            for (int i = 0; i < speechs.Length; i++)
            {
                dialogueManager.GetDialogueTextUI().text = speechs[i];
                yield return new WaitForSeconds(_countProvisorio);
                if (cameraToDesactive) cameraToDesactive.SetActive(false);
                if (item) playerInventory.AddItem(new Item(this.item), 1);
            }

            if (onlyOnce) alreadyExecuted = true;

        }
        // if(dialogueSound != null)
        // dialogueSound.StopSound();

        dialogueManager.GetDialogueBox().SetActive(false);

        if (nextDialogueScript != null)
        {
            isSomeDialogueRunning = true; // tava false antes
        }
        else if (nextDialogueScript == null)
        {
            isSomeDialogueRunning = false; // tava true antes
        }

        isSomeDialogueRunning = false;

        if (nextDialogueScript != null)
        {
            nextDialogueScript.RunCoroutine();
        }
        else if (nextDialogueScript == null && restrictCharMovement)
        {
            playerController.TurnPlayerControllerOn();
        }
        else
        {
            playerController.TurnPlayerControllerOn();
        }

        if (endSound) dialogueSound.PlayOneShoot2D();

        if (doorInteract) doorInteract.canUse = true;
    }

    public void RunCoroutine() { StartCoroutine(SpeechCoroutine()); }

    public IEnumerator SpeechCoroutine()
    {
        yield return new WaitUntil(DialogueIsRunning);
        StartCoroutine(Speech());
    }

    public bool DialogueIsRunning()
    {
        return !isSomeDialogueRunning;
    }

    private float CalculateSpeechTime(string speechTotalLetters)
    {
        float totalTime = 0;
        foreach (char letters in speechTotalLetters)
        {
            if (letters != ' ') totalTime += 0.2f;
        }
        if (totalTime < 1f) totalTime = 2.5f;
        return totalTime;
    }
}
