using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleQuatroAnjosResolucao : MonoBehaviour
{
    // quando ocorre a resolução do puzzle, mudar a cor dos emissions dos mecanismos de roxo pra laranja. -> funciona
    // O espirito do último chefe foi então selado. // Precisa fazer quando juntar na cena final
    

    [Header("Change Emission Color")]
    [SerializeField] private List<Renderer> mechaRends = new List<Renderer>(); // vamos usar pra alterar a cor do emission
    //[SerializeField] private Color emissionPurple;
    [SerializeField] private Color EmissionOrange;

    [SerializeField] private RotationMechanismBase rotationMechanism;

    private void Start()
    {
        rotationMechanism.puzzleSolvedDelegate -= MakeEmissionOrange;
        rotationMechanism.puzzleSolvedDelegate += MakeEmissionOrange;
    }

    private void MakeEmissionOrange()
    {
        mechaRends = rotationMechanism.GetMechanismRenderers();
        for(int i = 0; i < mechaRends.Count; i++)
        {
            mechaRends[i].material.SetColor("_EmissionColor", EmissionOrange * 5.5f);
        }
    }
}
