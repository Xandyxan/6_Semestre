using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruteiraInteraction : MonoBehaviour, IInteractable, ISelectable
{
    // Primeiramente, o jogador precisará acender 7 velas. Quando o contador de velas acesas indicar que todas foram acendidas, a próxima etapa do puzzle será liberada.

    // Quando as velas estiverem acesas, o NPC fantasma faminto irá aparecer e resmungar que está com fome.
    // Caso o player interaja com a fruteira, será apresentada a opção de usar o item das frutas, realizando a oferenda ao fantasma faminto. As frutas serão consumidas
    // e o fantasma entregará a chave do prédio de velar para o player.
    // Feedback do fantasma satisfeito encerra o puzzle.
    public delegate void CandleLitDelegate();
    public CandleLitDelegate candleLitEvent;

    public delegate void OferendaDelegate();
    public OferendaDelegate oferendaEvent;

    [Header("Acender velas")]
    [SerializeField] private int candleCounter;
    private bool candlesLit;

    [Header("Fantasma Obscuro")]
    [Tooltip("NPC inimigo que realiza patrol pela sala")]
    [SerializeField] private GameObject obscuroInimigo;
    [Tooltip("NPC acting que irá ficar resmungando")]
    [SerializeField] private GameObject famintoNPC;
    [SerializeField] private GameObject damageArea;

    [Header("Camera")]
    [SerializeField] private Cinemachine.CinemachineVirtualCamera interactionCam;
    private Camera mainCam;
    [SerializeField] private GameObject player; // we will temporally disable the player gameobject when using the inspection camera. Need to rework later!

    private bool isInteracting = false; // we will use this to disable player control, change to interaction camera and register correct input when interacting.
    private bool firstInput = true; // buffer pra evitar do objeto deselecionar no momento que usa o comando de selecionar;
    private bool solved = false;

    [Header("Selection")]
    [Tooltip("The visual feedback for an item that can be interacted with")]
    [SerializeField] private GameObject interactionFeedback;

    [Header("Item necessário")]
    [SerializeField] private string item; // mudar isso pro tipo de var que o xandy usa pra definir os itens

    [Header("Recompensa do Puzzle")]
    [SerializeField] private GameObject chaveVelar;

    [Header("New")]
    [SerializeField] private GameObject smallInventory;
    [SerializeField] private GameObject[] fruits;

    private void Awake()
    {
        mainCam = Camera.main;
    }
    private void Start()
    {
        gameObject.tag = "Untagged";
        candleLitEvent += ChangeGhost;
    }

    public void Interact()
    {
        if (candlesLit)
        {
            if (isInteracting || solved) return;
            firstInput = true;
            isInteracting = true;
            Deselect();

            GameManager.instance.removePlayerControlEvent?.Invoke();
            HidePlayerLayer();
            interactionCam.gameObject.SetActive(true);
            interactionCam.Priority = 15;

            smallInventory.SetActive(true);
            GameManager.instance.SetLockCursor(false);
            GameManager.instance.isOnPuzzle = true;
            GameManager.instance.puzzleNumber = 1;

            print("Aparece opção do player realizar oferenda pro fantasma");
        }

        Invoke("SetFirstInputFalse", 0.1f);
    }
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E) && !firstInput) { StopInteracting(); }
    }
    private void StopInteracting()
    {
        if (!isInteracting) { return; }
        print("StoppedInteracting");

        Select();
        interactionCam.gameObject.SetActive(false);

        isInteracting = false;

        smallInventory.SetActive(false);
        GameManager.instance.returnPlayerControlEvent?.Invoke();
        GameManager.instance.SetLockCursor(true);
        GameManager.instance.isOnPuzzle = false;
        GameManager.instance.puzzleNumber = -1;

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
        //mainCam.cullingMask &= ~(1 << LayerMask.NameToLayer("Player"));
        // playerController.SetCanMove(false);
    }

    private void ShowPlayerLayer()
    {
        //playerController.SetCanMove(true);
        //mainCam.cullingMask |= 1 << LayerMask.NameToLayer("Player");

    }

    private void SetFirstInputFalse()
    {
        firstInput = false;
    }

    public void AddCandleCounter(int value)
    {
        candleCounter += value;

        if(candleCounter == 7)
        {
            candlesLit = true;
            candleLitEvent();
        }
    }

    private void ChangeGhost()
    {
        gameObject.tag = "Selectable";
        damageArea.SetActive(false);
        obscuroInimigo.SetActive(false);
        famintoNPC.SetActive(true);
    }

    public void FinishPuzzle()
    {
        solved = true;
        foreach(GameObject fruits in fruits)
        {
            fruits.SetActive(true);
            chaveVelar.SetActive(true);
        }
    }
}
