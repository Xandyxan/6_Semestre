using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleInteraction : MonoBehaviour, IInteractable, ISelectable
{
    [Header("LightSource")]
    [SerializeField] protected GameObject lightSource; // a luz gerada pela vela
    [Tooltip("if the candle starts lit or not")]
    [SerializeField] protected bool candleLit;

    [Header("Selectable")]
    [SerializeField] protected GameObject interactionFeedback;
    public virtual void Interact()
    {
        Deselect();
        candleLit = !candleLit;
        if (candleLit)
        {
            lightSource.SetActive(true);
        }
        else
        {
            lightSource.SetActive(false);
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
