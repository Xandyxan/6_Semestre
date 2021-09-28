using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTMarcarPlayer : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;

        PlayerTestBT player = GameObject.FindObjectOfType<PlayerTestBT>();

        NPC npc = bt.GetComponent<NPC>();
        if (player)
        {
            npc.player = player;
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
