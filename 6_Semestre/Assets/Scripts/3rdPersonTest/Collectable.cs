using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable, ISelectable
{
    [Header("Selection")]
    [Tooltip("The visual feedback for an item that can be interacted with")]
    [SerializeField] private GameObject interactionFeedback;
    public ItemObject item;
    [SerializeField] private InventoryObject playerInventory;

    private bool check;

    [Header("Others")]
    [SerializeField] private DoorInteract doorInteract;
    [SerializeField] private DialogueManager2 dialogueManager;
    [SerializeField] private int dialogueNumber;
    [SerializeField] private GameObject otherItem;

    [SerializeField] private bool hasItemToUnlock;
    [SerializeField] private ItemObject itemUnlock;

    private void Awake()
    {
        if (playerInventory == null) playerInventory = FindObjectOfType<InventoryObject>();
        check = false;
        //necessita refatoração
    }

    public void Interact()
    {
        if (!check)
        {
            if (!hasItemToUnlock)
            {
                playerInventory.AddItem(new Item(this.item), 1);
                this.gameObject.SetActive(false);
                check = true;

                if (doorInteract) doorInteract.canUse = true;
                if (dialogueManager) dialogueManager.ExecuteDialogue(dialogueNumber);
                if (otherItem) otherItem.SetActive(true);
            }
            else
            {
                if(playerInventory.FindItemOnInventory2(itemUnlock.data.Id))
                {
                    playerInventory.RemoveItem(itemUnlock.data, -1);
                    playerInventory.AddItem(new Item(this.item), 1);
                    this.gameObject.SetActive(false);
                    check = true;
                }
                else
                {
                    if (dialogueManager) dialogueManager.ExecuteDialogue(dialogueNumber);
                }
            }

        }
        //Deselect();
    }

    public void Deselect()
    {
        if (interactionFeedback)
        {
            interactionFeedback.SetActive(false);
        }
    }

    public void Select()
    {
        if (interactionFeedback)
        {
            interactionFeedback.SetActive(true);
        }
    }
}
