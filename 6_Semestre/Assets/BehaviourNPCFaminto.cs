using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BehaviourNPCFaminto : MonoBehaviour
{
    public Text worldSpaceText;
    private bool oferendaRecebida;

    [SerializeField] private FruteiraInteraction puzzleFruteira;

    private void Start()
    {
        StartCoroutine(Resmungar());
        puzzleFruteira.oferendaEvent += RecebeuOferenda;
    }
   
    private IEnumerator Resmungar()
    {
        while (!oferendaRecebida)
        {
            worldSpaceText.text = "Eu estou com fome...";
            yield return new WaitForSeconds(2f);
            worldSpaceText.text = "...";
            yield return new WaitForSeconds(1);
            worldSpaceText.text = "Eu sinto fome...";
            yield return new WaitForSeconds(2f);
            worldSpaceText.text = "...";
            yield return new WaitForSeconds(1);
            worldSpaceText.text = "Dói muito...";
            yield return new WaitForSeconds(2f);
            worldSpaceText.text = "...";
            yield return new WaitForSeconds(1);
            worldSpaceText.text = "Me traga comida";
            yield return new WaitForSeconds(2f);
            worldSpaceText.text = "...";
            yield return new WaitForSeconds(1);
            worldSpaceText.text = "e eu lhe darei o que quer em troca";
            yield return new WaitForSeconds(3f);
            worldSpaceText.text = "...";
            yield return new WaitForSeconds(1);
        }
        worldSpaceText.text = "Obrigado";
        
    }

    private void RecebeuOferenda()
    {
        oferendaRecebida = true;
    }
}
