using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleInteractionFaminto : CandleInteraction
{
    [SerializeField] private FruteiraInteraction puzzleFruteira;
    public override void Interact()
    {
        Deselect();
        candleLit = !candleLit;
        if (candleLit)
        {
            lightSource.SetActive(true);
            puzzleFruteira.AddCandleCounter(1);
        }
        else
        {
            lightSource.SetActive(false);
            puzzleFruteira.AddCandleCounter(-1);
        }
    }

}
