using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTMarcarPlayer : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;

        PlayerStats playerStats = GameObject.FindObjectOfType<PlayerStats>();

        NPC npc = bt.GetComponent<NPC>();
        if (playerStats)
        {
            npc.player = playerStats;
            status = Status.SUCCESS;
        }
        else
        {
            npc.player = null;
            status = Status.FAILURE;
        }
        Print(bt);
        yield break;
    }
}
