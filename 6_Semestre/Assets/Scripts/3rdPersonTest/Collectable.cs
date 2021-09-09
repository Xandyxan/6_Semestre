using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable, ISelectable
{
    [Header("Selection")]
    [Tooltip("The visual feedback for an item that can be interacted with")]
    [SerializeField] private GameObject interactionFeedback;
    [SerializeField] private ItemObject _item;
    [SerializeField] private PlayerInventory playerInventory;

    public ItemObject item { get => _item; set => _item = value; }

    private void Awake()
    {
        if (playerInventory == null) playerInventory = FindObjectOfType<PlayerInventory>();
        //necessita refatoração
    }

    public void Interact()
    {
        playerInventory.CollectItem(_item);
        this.gameObject.SetActive(false);
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
        if(interactionFeedback)
        {
            interactionFeedback.SetActive(true);
        }
    }
}
