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
                status = Status.SUCCESS;
            }
        }
       
        Print(bt);
        yield break;
    }
}
