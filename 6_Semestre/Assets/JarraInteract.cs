using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarraInteract : MonoBehaviour, ISelectable, IInteractable
{
    //[Header("Cameras")]
    //[SerializeField] private Cinemachine.CinemachineVirtualCamera statueCamera;
    //[SerializeField] private Cinemachine.CinemachineVirtualCamera padreCamera;

    [Header("Selection")]
    [Tooltip("The visual feedback for an item that can be interacted with")]
    [SerializeField] private GameObject interactionFeedback;

    [Header("Others")]
    [SerializeField] private GameObject key;

    private MoveUp moveUp;

    private void Awake()
    {
        moveUp = GetComponent<MoveUp>();
    }

    public void Deselect()
    {
        interactionFeedback.SetActive(true);
    }

    public void Interact()
    {
        moveUp.RunCoroutine();
        key.SetActive(true);
    }

    public void Select()
    {
        interactionFeedback.SetActive(false);
    }

    //private void StartIntere
}
