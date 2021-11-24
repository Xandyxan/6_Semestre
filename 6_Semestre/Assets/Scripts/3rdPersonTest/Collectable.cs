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
            playerInventory.AddItem(new Item(this.item), 1);
            this.gameObject.SetActive(false);
            check = true;
        }
        //Deselect();
    }

    private void CollectItem(Collectable _item)
    {

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
