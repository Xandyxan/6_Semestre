using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleInteraction : MonoBehaviour, IInteractable, ISelectable
{
    [Header("LightSource")]
    [SerializeField] private GameObject luz; // a luz gerada pela vela
    [Tooltip("if the candle starts lit or not")]
    [SerializeField] private bool candleLit;

    [Header("Selectable")]
    [SerializeField] private GameObject interactionFeedback;
    public void Interact()
    {
        Deselect();
        candleLit = !candleLit;
        if (candleLit)
        {
            luz.SetActive(true);
        }
        else
        {
            luz.SetActive(false);
        }
    }

    public void Select()
    {
        interactionFeedback.SetActive(true);
    }

    public void Deselect()
    {
        interactionFeedback.SetActive(false);
    }
}
