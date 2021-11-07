using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColecaoFrutasInteraction : MonoBehaviour
{
    // Em primeiro momento, ao interagir com esse objeto, o player pode apenas inspeciona-lo. Quando o evento das velas acesas ocorrer, este script é desligado e o
    // script de item coletavel para a coleção de frutas é ativado.

    [SerializeField] private FruteiraInteraction puzzleFruteira;

    private CollectItemInteraction interacaoColetavel;

    private void Awake()
    {
        interacaoColetavel = GetComponent<CollectItemInteraction>();
        interacaoColetavel.enabled = false;
    }

    private void Start()
    {
        puzzleFruteira.candleLitEvent += ChangeItemBehaviour;
    }

    private void ChangeItemBehaviour()
    {
        interacaoColetavel.enabled = true;
        print("Player can now collect the fruits");
        this.enabled = false;
    }
}
