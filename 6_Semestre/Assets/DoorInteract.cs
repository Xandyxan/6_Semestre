using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteract : MonoBehaviour, ISelectable, IInteractable 
{
    public bool canUse;
    [SerializeField] GameObject textFeedback;
    [SerializeField] private int sceneToLoad;
    [SerializeField] private DialogueManager2 dialogueManager;
    [SerializeField] private int dialogueIndex;

    public void Deselect()
    {
        textFeedback.SetActive(false);
    }

    public void Interact()
    {
        if(canUse)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            dialogueManager.ExecuteDialogue(dialogueIndex);
        }
    }

    public void Select()
    {
        textFeedback.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
