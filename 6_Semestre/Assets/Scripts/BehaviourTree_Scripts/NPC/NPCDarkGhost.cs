using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDarkGhost : NPC
{

    protected override void Start()
    {
        BTSequence segueLuz = new BTSequence();
        segueLuz.children.Add(new BTTemLightSource());
        segueLuz.children.Add(new BTLightSourceProximo());
        segueLuz.children.Add(new BTMoverAteFonteLuz());

        BTSequence terrorRadius = new BTSequence();
        terrorRadius.children.Add(new BTTemPlayer());
        terrorRadius.children.Add(new BTPlayerProximo());
        terrorRadius.children.Add(new BTMarcarPlayer());
        terrorRadius.children.Add(new BTTiraSanidadeEmRaio());

        BTInversor naoPlayerProximo = new BTInversor();
        naoPlayerProximo.child = new BTPlayerProximo();

        BTInversor naoLightSourceProximo = new BTInversor();
        naoLightSourceProximo.child = new BTLightSourceProximo();
     

        BTSequenceParalelo moveToWaypoint = new BTSequenceParalelo();
        moveToWaypoint.children.Add(naoPlayerProximo);
        moveToWaypoint.children.Add(naoLightSourceProximo);
        moveToWaypoint.children.Add(new BTMoveToRandomWaypoint());

        BTSequence patrol = new BTSequence();
        patrol.children.Add(new BTTemWaypoints());
       // patrol.children.Add(new BTSelecionarWaypoints());
        patrol.children.Add(moveToWaypoint);

        BTSelector selecao = new BTSelector();
        selecao.children.Add(terrorRadius);
        selecao.children.Add(segueLuz);
        selecao.children.Add(patrol);

        base.Start();
        bt.root = selecao;

        StartCoroutine(bt.Begin());
    }
}
