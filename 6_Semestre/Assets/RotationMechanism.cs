using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationMechanism : MonoBehaviour
{
    #region Resumo do objetivo
    // ter X(8) rotações possiveis, quando o jogador interagir, alterar a rotação atual do objeto para a próxima da list. A lista está em loop, então quando chegar no
    // no último ou primeiro elemento, ela reinicia.
    // Seleção entre mecanismos: Teclas W ou seta pra cima alternam para a peça superior, Teclas S ou seta pra baixo alterna pra peça inferior. Quando a peça está
    // selecionada, Teclas A e seta pra esquerda rotacionam a peça para a esquerda e Teclas D e seta pra direita rotacionam a peça pra direita.
    #endregion
    [SerializeField] RotationMechanismBase mecBase;
    // transformar quantidade de rotação em um float, colocar bool pra apenas intergir caso o mecanismo esteja selecionado

    [SerializeField] private List<Vector3> rotations = new List<Vector3>(); // talvez alterar para uma list de quaternions dependendo
    [SerializeField] private Vector3 atualRotation; // igual ao transform.rotation
    private int[] posicoes = new int[8];
    private int atualPos = 0;

    [Header("Rotation Amount")]
    [SerializeField] private float rotAmount;
    [Space]
    [SerializeField] private bool isSelected;

    [Header("Mechanism Type")]
    [SerializeField] bool isHorizontal; // caso as peças estejam na horizontal, a rotação ocorre em x, caso não, a rotação ocorre em y.

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
    private void ChangeRotation(int dir) // diração -1 roda pra esquerda, direção +1 roda pra direita.
    {    
        atualPos += dir;
        if(atualPos >= posicoes.Length) { atualPos = posicoes[0];}
        else if(atualPos < 0) { atualPos = posicoes[posicoes.Length -1]; }
        print("Posicao Atual" + atualPos +" "+ rotations[atualPos]);
       
        atualRotation = rotations[atualPos];
        transform.rotation = Quaternion.Euler(atualRotation);

        mecBase.CheckPuzzleConclusion(); // ver se isso não vai deixar o jogo muito pesado, talvez testar usando delegates ou coisa do tipo
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
