using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTPlayerProximo : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;
        PlayerStats playerStats = GameObject.FindObjectOfType<PlayerStats>();
        NPC npc = bt.GetComponent<NPC>();
        if (playerStats)
        {
            if (Vector3.Distance(bt.transform.position, playerStats.transform.position) < npc.profile.playerDtRange)
            {
                npc.player = playerStats; // antes tava num BT separado, mas acho que só tava pesando a toa, dai juntei aqui. Qualquer coisa, colocar fora da detecção de distância pra ter a ref.
                status = Status.SUCCESS;
            }
            else
            {
                npc.player = null;
            }
        }
       
        Print(bt);
        yield break;
    }
}
