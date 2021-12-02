using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteract : MonoBehaviour, ISelectable, IInteractable 
{
    public bool canUse;
    [SerializeField] private InventoryObject playerInventory;
    [SerializeField] private ItemObject itemUnlock;

    [SerializeField] GameObject textFeedback;
    [SerializeField] private int sceneToLoad;
    [SerializeField] private DialogueManager2 dialogueManager;
    [SerializeField] private int dialogueIndex;

    public void Deselect()
    {
        if (textFeedback) textFeedback.SetActive(false);
    }

    public void Interact()
    {
        if (playerInventory != null && playerInventory.FindItemOnInventory2(itemUnlock.data.Id))
        {
            SceneManager.LoadScene(sceneToLoad);
            //playerInventory.RemoveItem(itemUnlock.data, -1);
        }
        else if(canUse)
        {
            SceneManager.LoadScene(sceneToLoad);
            
        }
        else if(!canUse || playerInventory != null || playerInventory.FindItemOnInventory2(itemUnlock.data.Id))
        {
            if(dialogueManager) dialogueManager.ExecuteDialogue(dialogueIndex);
        }
    }

    public void Select()
    {
        if(textFeedback) textFeedback.SetActive(true);
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
