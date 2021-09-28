using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTFollowPlayer : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.RUNNING;
        Print(bt);
        NPC npc = bt.GetComponent<NPC>();

        if (npc.player)
        {
            while (npc.player.gameObject.activeInHierarchy)
            {
                npc.npcAgent.SetDestination(npc.player.transform.position);

                if (npc.npcAgent.remainingDistance < npc.profile.atackRange)
                {
                    npc.npcAgent.SetDestination(npc.transform.position);
                    status = Status.SUCCESS;
                    break;
                }else if(npc.npcAgent.remainingDistance > npc.profile.playerDtRange)
                {
                    status = Status.FAILURE;
                    break;
                }
                yield return null;
            }
        }

        if (status == Status.RUNNING)
        {
            status = Status.FAILURE;
            Print(bt);
        }
    }
}
