using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PortaoInteraction : MonoBehaviour, IInteractable, ISelectable
{
    [Header("Abrir Portoes")]
    private Animator portaoAnim;
    [Tooltip("Outside, Inside")]
    [SerializeField] private string portaoSide;
    [Space]
    [SerializeField] private GameObject chaveObj;
    [SerializeField] private GameObject canvasChaveConsumida;
    private Animator keyAnimator;

    [Header("Camera")]
    [SerializeField] private Cinemachine.CinemachineVirtualCamera interactionCam;

    [Header("Scene Index")]
    [SerializeField] int sceneToLoadIndex;

    [Header("Position on Scene transition")]
    [Tooltip("A posição em que o player começa ao carregar outra cena. Ex: outro lado de uma porta, etc")]
    [SerializeField] float transPosX;
    [SerializeField] float transPosY;
    [SerializeField] float transPosZ;

    [Header("Fade")]
    [SerializeField] private FadeImage fadeScript;

    [Header("Selection")]
    [Tooltip("The visual feedback for an item that can be interacted with")]
    [SerializeField] private GameObject interactionFeedback;

    private bool gateUnlocked = false;
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
        keyAnimator = chaveObj.GetComponent<Animator>();
        portaoAnim = GetComponent<Animator>();
        PlayerPrefs.SetInt("PortaoNobre", 0);
    }
    private void Start()
    {
        int valor = PlayerPrefs.GetInt("PortaoNobre", 0);
        if(valor == 1)
        {
            gateUnlocked = true;
        }
        else
        {
            gateUnlocked = false;
        }
    }
    private IEnumerator UnlockGate()
    {
        // método que é chamado apenas na primeira vez que o player interage com o portão, aparece o feedback de que o item da chave foi consumido e tals.
        chaveObj.SetActive(true);
        keyAnimator.SetTrigger("Used");
        yield return new WaitForSeconds(0.8f);
        canvasChaveConsumida.SetActive(true);
        yield return new WaitForSeconds(.8f);
        portaoAnim.SetTrigger(portaoSide);
        yield return new WaitForSeconds(1.3f);
        PlayerPrefs.SetInt("PortaoNobre", 1); // 0 trancado, 1 destrancado
        Fade();
        yield return null;
    }

    private IEnumerator OpenGate()
    {
        // método chamado quando o portão já estiver destrancado. Apenas roda os eventos de abertura do portão e o fade load pra cena devida.
        portaoAnim.SetTrigger(portaoSide);
        yield return new WaitForSeconds(.5f);
        Fade();
        yield return null;
        // se pa fazer a camera dar zoom / mover aqui 
    }
    public void Interact()
    {
        GameManager.instance.removePlayerControlEvent?.Invoke();
        HidePlayerLayer();
        SetSceneStartPos();
        interactionCam.Priority = 15;
        Deselect();
        if (gateUnlocked)
        {
            StartCoroutine(OpenGate());
        }
        else
        {
            StartCoroutine(UnlockGate());
        }
    }

    public void Fade()
    {
        fadeScript.SetFadeIn(true);
        fadeScript.SetHasNextFade(false);
        fadeScript.SetHasSceneLoad(true);
        fadeScript.SetSceneIndex(sceneToLoadIndex);

        fadeScript.StartCoroutine(fadeScript.Fade(2.5f));
    }
    public void Select()
    {
        interactionFeedback.SetActive(true);
    }

    public void Deselect()
    {
        interactionFeedback.SetActive(false);
    }

    private void SetSceneStartPos()
    {
        PlayerPrefs.SetFloat("PlayerStartPosX", transPosX);
        PlayerPrefs.SetFloat("PlayerStartPosY", transPosY);
        PlayerPrefs.SetFloat("PlayerStartPosZ", transPosZ);
    }

    private void HidePlayerLayer() // se pa vamos precisar chamar um ShowPlayerLayer antes do final do fade
    {
        mainCam.cullingMask &= ~(1 << LayerMask.NameToLayer("Player"));
    }

    private void ShowPlayerLayer()
    {
        mainCam.cullingMask |= 1 << LayerMask.NameToLayer("Player");
    }
}
