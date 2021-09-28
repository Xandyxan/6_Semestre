using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPaleGhost : NPC
{
    [Space]
    public GameObject hitbox;
    public Transform lightSource;

    protected override void Start()
    {
        BTSequence evitaLuz = new BTSequence();
        evitaLuz.children.Add(new BTTemLightSource());
        evitaLuz.children.Add(new BTLightSourceProximo());
        evitaLuz.children.Add(new BTMarcaLightSourceProximo());
        evitaLuz.children.Add(new BTEvitaLuz());
        
        BTInversor naoPlayerProximo = new BTInversor();
        naoPlayerProximo.child = new BTPlayerProximo();

        BTInversor naoLightSourceProximo = new BTInversor();
        naoLightSourceProximo.child = new BTLightSourceProximo();

        BTSequenceParalelo moveToWaypoint = new BTSequenceParalelo();
        moveToWaypoint.children.Add(naoPlayerProximo);
        moveToWaypoint.children.Add(naoLightSourceProximo);
        moveToWaypoint.children.Add(new BTMoveToRandomWaypoint());

        BTSequenceParalelo aproachPlayer = new BTSequenceParalelo();
        aproachPlayer.children.Add(naoLightSourceProximo);
        aproachPlayer.children.Add(new BTFollowPlayer());

        BTSequence attackPlayer = new BTSequence();
        attackPlayer.children.Add(new BTTemPlayer());
        attackPlayer.children.Add(new BTPlayerProximo());
        attackPlayer.children.Add(new BTMarcarPlayer());
        attackPlayer.children.Add(aproachPlayer);
        attackPlayer.children.Add(new BTActivateHitBox());

        BTSequence patrol = new BTSequence();
        patrol.children.Add(new BTTemWaypoints());
        patrol.children.Add(new BTSelecionarWaypoints());
        patrol.children.Add(moveToWaypoint);

        BTSelector selecao = new BTSelector();
        selecao.children.Add(evitaLuz);
        selecao.children.Add(patrol);
        selecao.children.Add(attackPlayer);

        base.Start();
        bt.root = selecao;

        StartCoroutine(bt.Begin());
    }

    public void DeactivateGhost()
    {
        gameObject.SetActive(false);
    }
}
