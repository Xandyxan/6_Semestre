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
                //npc.npcAgent.speed = .8f;
                npc.npcAgent.SetDestination(npc.player.transform.position);
                npc.npcAnim.SetBool("IsWalking", true);
                if (npc.npcAgent.remainingDistance <= npc.npcAgent.stoppingDistance)
                {
                    // npc.npcAgent.SetDestination(npc.transform.position); 
                    npc.npcAnim.SetBool("IsWalking", false);
                    status = Status.SUCCESS;
                    break;
                }else if(npc.npcAgent.remainingDistance > npc.profile.playerDtRange)
                {
                    status = Status.FAILURE;
                   // npc.npcAgent.speed = 0.5f;
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
