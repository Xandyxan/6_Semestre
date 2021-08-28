using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManagerTP : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(selectableTag))
        {
            print("Colidiu");
           
            if (Input.GetKeyDown(KeyCode.E))
            {
                var interactable = other.GetComponent<IInteractable>();
                if (interactable == null) return;
                interactable.Interact();
            }
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(selectableTag))
        {
            var selectable = other.gameObject.GetComponent<ISelectable>();
            if (selectable == null) return;
            selectable.Select();
        }    
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(selectableTag))
        {
            var selectable = other.gameObject.GetComponent<ISelectable>();
            if (selectable == null) return;
            selectable.Deselect();
        }
    }

    // Refatorar esse código, ele foi só feito pra teste inicial.
}
