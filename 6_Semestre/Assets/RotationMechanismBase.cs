using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationMechanismBase : MonoBehaviour, IInteractable, ISelectable
{
    // if the player clicks E, he starts interacting, clicking E again leaves. While interacting, W and S change between mechanisms(and A and D rotate them). If they
    // press Space, the puzzle conclusion check runs and gives him feedback on if the sequence he tried was right or wrong.
    #region Delegates
    public delegate void PuzzleSolved();
    public PuzzleSolved puzzleSolvedDelegate;
    #endregion

    [SerializeField] List<RotationMechanism> mechanisms = new List<RotationMechanism>();
    [Space]
    [Header("Condition")]
    [SerializeField] private List<int> mechanismCorrectPositions = new List<int>();

    private List<Renderer> mechanismRenderes = new List<Renderer>();

    private int selectionIndex = 0;
    private bool isInteracting = false; // we will use this to disable player control, change to interaction camera and register correct input when interacting.
    private bool firstInput = true; // buffer pra evitar do objeto deselecionar no momento que usa o comando de selecionar;

    [Header("Camera")]
    [SerializeField] private GameObject player; // we will temporally disable the player gameobject when using the inspection camera. Need to rework later!
    [SerializeField] private Cinemachine.CinemachineVirtualCamera interactionCam;
    private Camera mainCam;

    [Header("Selection")]
    [Tooltip("The visual feedback for an item that can be interacted with")]
    [SerializeField] private GameObject interactionFeedback;

    [SerializeField] private bool isHorizontal;

    private bool solved;

    private void Awake()
    {
        foreach (RotationMechanism mecha in mechanisms)
        {
            Renderer mechaRend = mecha.transform.GetComponent<Renderer>();
            mecha.SetIsSelected(false);
            mechanismRenderes.Add(mechaRend);
        }
        mainCam = Camera.main;
    }

  
    void Update()
    {
        if (isInteracting)
        {
            if (isHorizontal)
            {
                if (Input.GetKeyDown(KeyCode.A)) { ChoseAnother(-1); }
                else if (Input.GetKeyDown(KeyCode.D)) { ChoseAnother(1); }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.W)) { ChoseAnother(-1); }
                else if (Input.GetKeyDown(KeyCode.S)) { ChoseAnother(1); }
            }
          
            //if (Input.GetKeyDown(KeyCode.Space)) { CheckPuzzleConclusion(); }
        }
    }

    private void ChoseAnother(int DirToChose) // dirToChose will be either (-1) or (+1), the selected index will change based on that number.
    {
        if (mechanisms.Count > 0)
        {
            selectionIndex += DirToChose;

            if (selectionIndex > mechanisms.Count - 1) { selectionIndex = 0; }
            else if (selectionIndex < 0) { selectionIndex = mechanisms.Count - 1; }

          for(int i = 0; i < mechanisms.Count; i++)
            {
                mechanismRenderes[i].material.DisableKeyword("_EMISSION");
                mechanisms[i].SetIsSelected(false);
            }
            mechanismRenderes[selectionIndex].material.EnableKeyword("_EMISSION");
            mechanisms[selectionIndex].SetIsSelected(true);

          
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
        foreach (Renderer rend in mechanismRenderes)
        {
            rend.material.DisableKeyword("_EMISSION");
        }
        isInteracting = false;
        
        mechanisms[selectionIndex].SetIsSelected(false);
        GameManager.instance.returnPlayerControlEvent?.Invoke();
        
        ShowPlayerLayer();
        interactionCam.Priority = 5;
    }

    void IInteractable.Interact()
    {
        if (isInteracting || solved) { return; }
        print("Interagiu com o puzzle de sequencia");
        firstInput = true;
        isInteracting = true;
        interactionCam.Priority = 12;
        //player.SetActive(false); // mudar isso pra versão que só tira a layer do player do culling da camera e chama o evento de PlayerCannotMove
        GameManager.instance.removePlayerControlEvent?.Invoke();
        HidePlayerLayer();

        ChoseAnother(0);
        Invoke("SetFirstInputFalse", 0.1f);
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

    public bool CheckPuzzleConclusion()
    {
        int correctAnswers = 0;
        for(int i = 0; i< mechanisms.Count; i++)
        {
            if (mechanismCorrectPositions[i] - 1 == mechanisms[i].GetAtualPosition())
            {
                correctAnswers++;
            }
            else
            {
                print("Sequencia incorreta" + correctAnswers);
                return false; // caso um mecanismo já esteja errado, a tentativa conclusão do puzzle é dada como incorreta
            }
        }
        print("puzzle resolvido");
        // aqui chamamos o código de abrir o portão
        puzzleSolvedDelegate?.Invoke();

        solved = true;
        gameObject.tag = "Untagged";
        Deselect();
        StopInteracting();
        return (correctAnswers == mechanisms.Count); // se o jogador acertou a posição de 8 em 8 mecanismos, o puzzle é resolvido
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
