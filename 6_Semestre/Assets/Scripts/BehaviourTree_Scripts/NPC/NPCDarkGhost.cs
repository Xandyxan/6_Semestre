using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDarkGhost : NPC
{

    protected override void Start()
    {
        //BTSequence terrorRadius = new BTSequence();
        //terrorRadius.children.Add(new BTTemPlayer());
        //terrorRadius.children.Add(new BTPlayerProximo());
        //terrorRadius.children.Add(new BTMarcarPlayer());
        //terrorRadius.children.Add(new BTTiraSanidadeEmRaio());

        BTInversor naoPlayerProximo = new BTInversor();
        naoPlayerProximo.child = new BTPlayerProximo();

        BTInversor naoLightSourceProximo = new BTInversor();
        naoLightSourceProximo.child = new BTLightSourceProximo();

        //BTSequenceParalelo moveToLight = new BTSequenceParalelo();
        //moveToLight.children.Add(new BTMoverAteFonteLuz());

        BTSequenceParalelo aproachPlayer = new BTSequenceParalelo();
        aproachPlayer.children.Add(naoLightSourceProximo);
        aproachPlayer.children.Add(new BTFollowPlayer()); // teoricamente isso funciona, talvez mudar pra um em que ele não se move, apenas rotaciona para a direção do player sla

        BTSequence segueLuz = new BTSequence();
        segueLuz.children.Add(new BTTemLightSource());
        segueLuz.children.Add(new BTLightSourceProximo());
        segueLuz.children.Add(new BTMarcaLightSourceProximo());
        segueLuz.children.Add(new BTMoverAteFonteLuz());
        //segueLuz.children.Add(moveToLight);

        BTSequence attackPlayer = new BTSequence(); // da uma porrada no player caso ele esteja colado no fantasma. Não desaparece após acertar o jogador.
        attackPlayer.children.Add(new BTTemPlayer());
        attackPlayer.children.Add(new BTPlayerProximo());
        attackPlayer.children.Add(aproachPlayer);
        attackPlayer.children.Add(new BTActivateHitBox());


        BTSequenceParalelo moveToWaypoint = new BTSequenceParalelo();
        moveToWaypoint.children.Add(naoPlayerProximo);
        moveToWaypoint.children.Add(naoLightSourceProximo);
        moveToWaypoint.children.Add(new BTMoveToRandomWaypoint());

        BTSequence patrol = new BTSequence();
        patrol.children.Add(new BTTemWaypoints());
       // patrol.children.Add(new BTSelecionarWaypoints());
        patrol.children.Add(moveToWaypoint);

        BTSelector selecao = new BTSelector();
        //selecao.children.Add(terrorRadius); // o dano em área (-0.5 de sanidade por segundo)
        selecao.children.Add(segueLuz);
        selecao.children.Add(attackPlayer); // a porrada melee
        selecao.children.Add(patrol);

        base.Start();
        bt.root = selecao;

        StartCoroutine(bt.Begin());
    }
}
