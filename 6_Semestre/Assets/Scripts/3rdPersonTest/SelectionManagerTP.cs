using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManagerTP : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    private Collectable collectable;

    private void Update()
    {
        

        //Debug.Log(interactable);
    }

    private void OnTriggerStay(Collider col)
    {
        collectable = col.GetComponent<Collectable>();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (collectable != null)
            {
                collectable.Interact();
                collectable = null;
            }
            else
            {
                Debug.Log("The player is not close to any interactable item");
            }
        }

        if (col.gameObject.CompareTag(selectableTag))
        {
            var selectable = col.gameObject.GetComponent<ISelectable>();

            if (selectable != null)
            {
                selectable.Select();
            }
            else
            {
                Debug.LogError("There is no ISelectable interface into the object/script");
            }

        }

    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag(selectableTag))
        {
            var selectable = col.gameObject.GetComponent<ISelectable>();
            //interactable = null;

            if (selectable != null)
            {
                selectable.Deselect();
            }
            else
            {
                Debug.LogError("There is no ISelectable interface into the object/script");
            }

        }
    }

    // Refatorar esse código, ele foi só feito pra teste inicial.
}
