using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationMechanism : MonoBehaviour
{
    #region Resumo do objetivo
    // ter X(8) rota��es possiveis, quando o jogador interagir, alterar a rota��o atual do objeto para a pr�xima da list. A lista est� em loop, ent�o quando chegar no
    // no �ltimo ou primeiro elemento, ela reinicia.
    // Sele��o entre mecanismos: Teclas W ou seta pra cima alternam para a pe�a superior, Teclas S ou seta pra baixo alterna pra pe�a inferior. Quando a pe�a est�
    // selecionada, Teclas A e seta pra esquerda rotacionam a pe�a para a esquerda e Teclas D e seta pra direita rotacionam a pe�a pra direita.
    #endregion
    [SerializeField] RotationMechanismBase mecBase;
    // transformar quantidade de rota��o em um float, colocar bool pra apenas intergir caso o mecanismo esteja selecionado

    [SerializeField] private List<Vector3> rotations = new List<Vector3>(); // talvez alterar para uma list de quaternions dependendo
    [SerializeField] private Vector3 atualRotation; // igual ao transform.rotation
    private int[] posicoes = new int[8];
    private int atualPos = 0;

    [Header("Rotation Amount")]
    [SerializeField] private float rotAmount;
    [Space]
    [SerializeField] private bool isSelected;

    [Header("Mechanism Type")]
    [SerializeField] bool isHorizontal; // caso as pe�as estejam na horizontal, a rota��o ocorre em x, caso n�o, a rota��o ocorre em y.

    private void Awake()
    {
        posicoes = new int[] { 0, 1, 2, 3, 4, 5, 6, 7};
        atualRotation = transform.rotation.eulerAngles;
        //rotations[0] = atualRotation;

        if (isHorizontal)
        {
            for (int i = 0; i < rotations.Count; i++)
            {
                rotations[i] = new Vector3(atualRotation.x + (rotAmount * i), atualRotation.y, atualRotation.z);
            }
        }
        else
        {
            for (int i = 0; i < rotations.Count; i++)
            {
                rotations[i] = new Vector3(atualRotation.x, atualRotation.y + (rotAmount * i), atualRotation.z);
            }
        }
        
    }
    private void Update()
    {
        if (isSelected)
        {
            if (isHorizontal)
            {
                if (Input.GetKeyDown(KeyCode.W)) { ChangeRotation(-1); }
                else if (Input.GetKeyDown(KeyCode.S)) { ChangeRotation(1); }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.A)) { ChangeRotation(-1); }
                else if (Input.GetKeyDown(KeyCode.D)) { ChangeRotation(1); }
            }
           
        } 
    }
    private void ChangeRotation(int dir) // dira��o -1 roda pra esquerda, dire��o +1 roda pra direita.
    {    
        atualPos += dir;
        if(atualPos >= posicoes.Length) { atualPos = posicoes[0];}
        else if(atualPos < 0) { atualPos = posicoes[posicoes.Length -1]; }
        print("Posicao Atual" + atualPos +" "+ rotations[atualPos]);
       
        atualRotation = rotations[atualPos];
        transform.rotation = Quaternion.Euler(atualRotation);

        mecBase.CheckPuzzleConclusion(); // ver se isso n�o vai deixar o jogo muito pesado, talvez testar usando delegates ou coisa do tipo
    }

    public void SetIsSelected(bool value)
    {
        isSelected = value;
    }
    public int GetAtualPosition()
    {
        return atualPos;
    }
}
