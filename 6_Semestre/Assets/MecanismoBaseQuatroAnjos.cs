using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanismoBaseQuatroAnjos : MonoBehaviour
{
    // quando ocorre a resolução do puzzle, mudar a cor dos emissions dos mecanismos de roxo pra laranja.
    // O espirito do último chefe foi então selado.
    // rotação terra = -90
    // rotação mar = 135
    // rotação árvore = -90
    // rotação sol = 45

    [Header("Change Emission Color")]
    [SerializeField] private List<Material> mecanismosAnjosMat = new List<Material>(); // vamos usar pra alterar a cor do emission
    [SerializeField] private Color emissionPurple;
    [SerializeField] private Color EmissionOrange;
}
