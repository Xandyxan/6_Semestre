using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTPlayerProximo : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        NPC npc = bt.GetComponent<NPC>();
        if (player)
        {
            if (Vector3.Distance(bt.transform.position, player.transform.position) < npc.profile.playerDtRange)
            {
                status = Status.SUCCESS;
            }
        }
       
        Print(bt);
        yield break;
    }
}
