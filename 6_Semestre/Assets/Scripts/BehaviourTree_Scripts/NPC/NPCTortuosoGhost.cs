using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // temp enquanto n�o temos som no jogo

public class NPCTortuosoGhost : NPC
{
    public string crySoundPath;
    public GameObject[] ghosts;
    public Text criesText;


    // patrol, emite choro. S�o guiados atrav�s do som, n�o notando o jogador caso ele esteja parado. Caso um som forte ocorra perto de si, ele ir� berrar alto, invocando outros fantasmas pelo ambiente.
    protected override void Start()
    {
        BTInversor NaoTemSoundSourceProximo = new BTInversor();
        NaoTemSoundSourceProximo.child = new BTTemSoundSource();

        BTSequenceParalelo moveToWaypoint = new BTSequenceParalelo();
        moveToWaypoint.children.Add(new BTMoveToRandomWaypoint());
        moveToWaypoint.children.Add(NaoTemSoundSourceProximo);

        BTSequence patrol = new BTSequence();

        patrol.children.Add(moveToWaypoint);
        patrol.children.Add(new BTCry());

        BTSequence reageASom = new BTSequence();
        reageASom.children.Add(new BTTemSoundSource());
        reageASom.children.Add(new BTSoundSourceProximo());
        reageASom.children.Add(new BTInvokeOtherGhosts());

        BTSelector selecao = new BTSelector();
        
        selecao.children.Add(patrol);
        selecao.children.Add(reageASom);
       

        base.Start();
        bt.root = selecao;

        StartCoroutine(bt.Begin());
    }
}
