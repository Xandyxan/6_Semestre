using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable, ISelectable
{


    [Header("Selection")]
    [Tooltip("The visual feedback for an item that can be interacted with")]
    [SerializeField] private GameObject interactionFeedback;

    public void Interact()
    {
        this.gameObject.SetActive(false);
        //Deselect();
    }

    public void Deselect()
    {
        interactionFeedback.SetActive(false);
    }

    public void Select()
    {
        interactionFeedback.SetActive(true);
    }
}
