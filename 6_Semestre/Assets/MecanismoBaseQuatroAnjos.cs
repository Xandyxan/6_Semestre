using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanismoBaseQuatroAnjos : MonoBehaviour
{
    // quando ocorre a resolu��o do puzzle, mudar a cor dos emissions dos mecanismos de roxo pra laranja.
    // O espirito do �ltimo chefe foi ent�o selado.
    // rota��o terra = -90
    // rota��o mar = 135
    // rota��o �rvore = -90
    // rota��o sol = 45

    [Header("Change Emission Color")]
    [SerializeField] private List<Material> mecanismosAnjosMat = new List<Material>(); // vamos usar pra alterar a cor do emission
    [SerializeField] private Color emissionPurple;
    [SerializeField] private Color EmissionOrange;
}
