using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCadaverEsguio : NPC
{
    // vagam lentamente pelo escuro das criptas, sendo criaturas capazes de causar morte instantânea caso alcance o jogador.

    // patrol e follow

    [Space]
    public GameObject hitbox;

    protected override void Start()
    {
        BTInversor naoPlayerProximo = new BTInversor();
        naoPlayerProximo.child = new BTPlayerProximo();

        BTSequenceParalelo moveToWaypoint = new BTSequenceParalelo();
        moveToWaypoint.children.Add(naoPlayerProximo);
        moveToWaypoint.children.Add(new BTMoveToRandomWaypoint());

        BTSequence patrol = new BTSequence();
        patrol.children.Add(new BTTemWaypoints());
        patrol.children.Add(new BTSelecionarWaypoints());
        patrol.children.Add(moveToWaypoint);

        BTSequence attackPlayer = new BTSequence();
        attackPlayer.children.Add(new BTTemPlayer());
        attackPlayer.children.Add(new BTPlayerProximo());
        attackPlayer.children.Add(new BTMarcarPlayer());
        attackPlayer.children.Add(new BTFollowPlayer());
        attackPlayer.children.Add(new BTActivateHitBox());

        BTSelector selecao = new BTSelector();
        selecao.children.Add(patrol);
        selecao.children.Add(attackPlayer);

        base.Start();
        bt.root = selecao;

        StartCoroutine(bt.Begin());
    }
}
