using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CollectItemInteraction : MonoBehaviour, IInteractable, ISelectable
{
    #region objetivos
    // objetivo do script: Ao interagir, troca-se para uma camera focada no objeto a ser coletado. Ele está destacado por um highlight. A opção de coletar o item
    // é apresentada ao jogador. Caso ele opte por coletar o item, o mesmo é desligado e o item é adicionado ao inventário. Caso o jogador cancele a coleta, a camera
    // retorna para a padrão e a interação acaba. Caso já tenha coletado o item, não é possivel interagir novamente com este objeto.
    #endregion

    [Header("Camera")]
    [SerializeField] private Cinemachine.CinemachineVirtualCamera interactionCam;
    private Camera mainCam;
    [SerializeField] private GameObject player; // we will temporally disable the player gameobject when using the inspection camera. Need to rework later!

    [SerializeField] private Renderer itemRenderer;

    private bool isInteracting = false; // we will use this to disable player control, change to interaction camera and register correct input when interacting.
    private bool firstInput = true; // buffer pra evitar do objeto deselecionar no momento que usa o comando de selecionar;

    [Header("Selection")]
    [Tooltip("The visual feedback for an item that can be interacted with")]
    [SerializeField] private GameObject interactionFeedback;

    public ItemObject item;
    [SerializeField] private GameObject itemObject;
    [SerializeField] private InventoryObject playerInventory;

    private bool canCollect;
    private bool itemCollected;

    private void Awake()
    {
        if (playerInventory == null) playerInventory = FindObjectOfType<InventoryObject>();
        mainCam = Camera.main; // substituir por uma var global no futuro
    }

    public void Interact()
    {
        if (isInteracting || itemCollected) return;
        firstInput = true;
        isInteracting = true;
        Deselect();

        GameManager.instance.removePlayerControlEvent?.Invoke();
        HidePlayerLayer();

        interactionCam.Priority = 15;
        itemRenderer.material.EnableKeyword("_EMISSION");
        // apresentar opção de coletar o item aqui
        canCollect = true;

        Invoke("SetFirstInputFalse", 0.1f);
    }

    private void Update()
    {
        if(canCollect && Input.GetKeyDown(KeyCode.Space))
        {
          //  playerInventory.AddItem(new Item(this.item), 1);
            playerInventory.AddItem(new Item(this.item), 1);
            gameObject.tag = "Untagged";
            itemCollected = true;
            GameManager.instance.endDemoEvent.Invoke(); // só pra demo aaaaaaaaaa
            StopInteracting();
            Deselect();
        }
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E) && !firstInput) { StopInteracting(); }
    }

    private void StopInteracting()
    {
        if (!isInteracting) { return; }
        print("StoppedInteracting");
        itemRenderer.material.DisableKeyword("_EMISSION");

        Select();
        interactionCam.Priority = 5;
      
        isInteracting = false;
        canCollect = false;

        GameManager.instance.returnPlayerControlEvent?.Invoke();

        ShowPlayerLayer();
    }

    private void SetFirstInputFalse()
    {
        firstInput = false;
    }

    public void Select()
    {
        interactionFeedback.SetActive(true);
    }

    public void Deselect()
    {
        interactionFeedback.SetActive(false);
    }

    private void ShowPlayerLayer()
    {
        //playerController.SetCanMove(true);
        mainCam.cullingMask |= 1 << LayerMask.NameToLayer("Player");

    }

    private void HidePlayerLayer()
    {
        mainCam.cullingMask &= ~(1 << LayerMask.NameToLayer("Player"));
        // playerController.SetCanMove(false);
    }
}
