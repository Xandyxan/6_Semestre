using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalCruzInteraction : MonoBehaviour, IInteractable, ISelectable
{
    #region objetivo
    // Ao interagir, a camera foca no pedestal. O player irá ser indicado a utilizar um item. Caso o player interaja com o pedestal utilizando o item da CRUZ LIMPA,
    // uma versão da cruz será inserida na abertura do pedestal, chamando o método de checar conclusão do puzzle fragmentado.
    // Caso a conclusão seja correta, abre a gaveta que carrega a chave dos fundos
    #endregion

    [Header("Camera")]
    [SerializeField] private Cinemachine.CinemachineVirtualCamera interactionCam;
    private Camera mainCam;
    [SerializeField] private GameObject player; // we will temporally disable the player gameobject when using the inspection camera. Need to rework later!

    private bool isInteracting = false; // we will use this to disable player control, change to interaction camera and register correct input when interacting.
    private bool firstInput = true; // buffer pra evitar do objeto deselecionar no momento que usa o comando de selecionar;
    private bool canLeavePuzzle = true;
    private bool solved = false;

    [Header("Selection")]
    [Tooltip("The visual feedback for an item that can be interacted with")]
    [SerializeField] private GameObject interactionFeedback;

    [Header("Conclusão Puzzle")]
    [SerializeField] RotationMechanismBase mechanismBase;
    [SerializeField] private Transform gaveta;
    [SerializeField] private GameObject chaveFundos; // vamos mudar a tag dela quando a gaveta abrir
    private Vector3 targetPos;

    private void Awake()
    {
        mainCam = Camera.main; // substituir por uma var global no futuro
    }
    public void Interact()
    {
        if (isInteracting || solved) return;
        firstInput = true;
        isInteracting = true;
        Deselect();

        GameManager.instance.removePlayerControlEvent?.Invoke();
        HidePlayerLayer();

        interactionCam.Priority = 15;

        if (mechanismBase.CheckPuzzleConclusion())
        {
            StartCoroutine(OpenDrawer(0.5f));
        }
        Invoke("SetFirstInputFalse", 0.1f);
    }
   
    private IEnumerator OpenDrawer(float openingDist)
    {
        canLeavePuzzle = false;
        interactionCam.LookAt = gaveta;
        targetPos = new Vector3(gaveta.position.x, gaveta.position.y, gaveta.position.z + openingDist);
        while(Vector3.Distance(gaveta.position, targetPos) >= 0.02f)
        {
            gaveta.position = Vector3.Lerp(gaveta.position, targetPos, Time.deltaTime * 0.8f);
            yield return null;
        }
        chaveFundos.tag = "Selectable";
        gameObject.tag = "Untagged";
        solved = true;
        StopInteracting();
        Deselect();
        yield break;

    }
    
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E) && !firstInput && canLeavePuzzle) { StopInteracting(); }
    }
    private void StopInteracting()
    {
        if (!isInteracting) { return; }
        print("StoppedInteracting");

        Select();
        interactionCam.Priority = 5;

        isInteracting = false;

        GameManager.instance.returnPlayerControlEvent?.Invoke();

        ShowPlayerLayer();
    }

    public void Select()
    {
        interactionFeedback.SetActive(true);
    }

    public void Deselect()
    {
        interactionFeedback.SetActive(false);
    }

    private void HidePlayerLayer()
    {
        mainCam.cullingMask &= ~(1 << LayerMask.NameToLayer("Player"));
        // playerController.SetCanMove(false);
    }

    private void ShowPlayerLayer()
    {
        //playerController.SetCanMove(true);
        mainCam.cullingMask |= 1 << LayerMask.NameToLayer("Player");

    }

    private void SetFirstInputFalse()
    {
        firstInput = false;
    }
}
