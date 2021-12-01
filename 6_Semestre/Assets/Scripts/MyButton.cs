using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ISelectHandler, IDeselectHandler, ISubmitHandler
{
    [Header("It is first button on Screen?")]
    [SerializeField] private bool itIsFirstButton;

    [Header("Graphics")]
    [SerializeField] private bool invertColors;
    private GameObject arrowBlack, arrowYellow;

    private bool selected = false;
    public static bool restart, canKeyboard;

    private void Awake()
    {
        if (!invertColors)
        {
            arrowBlack = transform.GetChild(1).gameObject;
            arrowYellow = transform.GetChild(2).gameObject;
        }
        else
        {
            arrowBlack = transform.GetChild(2).gameObject;
            arrowYellow = transform.GetChild(1).gameObject;
        }

        arrowBlack.SetActive(false);
        arrowYellow.SetActive(false);
        restart = true;
        canKeyboard = false;
    }

    private void Update()
    {
        if (itIsFirstButton && restart)
        {
            EventSystem.current.SetSelectedGameObject(this.gameObject);
            arrowYellow.SetActive(false);
            restart = false;
        }
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            canKeyboard = true;
        }

        if((Input.GetButtonDown("Submit") || Input.GetButtonDown("Vertical") || Input.GetButtonDown("Horizontal")) && canKeyboard)
        {
            restart = true;
            canKeyboard = false;
        }
    }

    //-----QUANDO O MOUSE PASSAR POR CIMA DESTE BOTAO-----//
    public void OnPointerEnter(PointerEventData eventData)
    {
        arrowBlack.SetActive(true);
        arrowYellow.SetActive(false);

        if (!EventSystem.current.alreadySelecting)
            EventSystem.current.SetSelectedGameObject(this.gameObject);
    }

    //-----QUANDO O MOUSE SAIR DE CIMA DESTE BOTAO-----//
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!selected)
        {
            arrowBlack.SetActive(false);
            arrowYellow.SetActive(false);
        }
    }

    //-----QUANDO O MOUSE CLICAR NESTE BOTAO-----//
    public void OnPointerDown(PointerEventData eventData)
    {
        arrowBlack.SetActive(false);
        arrowYellow.SetActive(true);
        canKeyboard = true;
    }

    //-----ENQUANTO O MOUSE NÃO ESTIVER CLICANDO NESTE BOTAO-----//
    public void OnPointerUp(PointerEventData eventData)
    {
        arrowBlack.SetActive(false);
        arrowYellow.SetActive(false);
    }

    //-----QUANDO ESTE BOTAO ESTIVER SELECIONADO (KEYBOARD INPUT)-----//
    public void OnSelect(BaseEventData eventData)
    {
        arrowBlack.SetActive(true);
        arrowYellow.SetActive(false);

        selected = true;
    }

    //-----QUANDO ESTE BOTAO ESTIVER DESLECIONADO (KEYBOARD INPUT)-----//
    public void OnDeselect(BaseEventData eventData)
    {
        arrowBlack.SetActive(false);
        arrowYellow.SetActive(false);

        selected = false;

        this.GetComponent<Selectable>().OnPointerExit(null);
    }

    //-----QUANDO ESTE BOTAO FOR PRESSIONADO (KEYBOARD INPUT)-----//
    public void OnSubmit(BaseEventData eventData)
    {
        arrowBlack.SetActive(false);
        arrowYellow.SetActive(true);
        Invoke("SetTochasRoxasFalse", 0.5f);
        MyButton.restart = true;
    }

    public void SetTochasRoxasFalse() { this.arrowYellow.SetActive(false); }
}
