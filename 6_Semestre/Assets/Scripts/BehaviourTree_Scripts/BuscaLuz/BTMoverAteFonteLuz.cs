using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTMoverAteFonteLuz : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.RUNNING;
        Print(bt);
        NPC npc = bt.GetComponent<NPC>();

        if(npc.lightSource!= null)
        {
            while (npc.lightSource.gameObject.activeInHierarchy == true)
            {
                npc.npcAgent.SetDestination(npc.lightSource.transform.position);
                npc.npcAnim.SetBool("FollowLight", true);

                if (npc.npcAgent.remainingDistance <= npc.npcAgent.stoppingDistance)
                {
                    status = Status.SUCCESS;
                    npc.npcAnim.SetBool("FollowLight", false);
                    yield break;
                }else if(npc.npcAgent.remainingDistance > npc.profile.lightSourceDtRange)
                {
                    status = Status.FAILURE;
                    npc.npcAnim.SetBool("FollowLight", false);
                    break;
                }
                yield return null;
            }
            npc.npcAgent.ResetPath();
        }
       

        if (status == Status.RUNNING)
        {
            npc.npcAnim.SetBool("FollowLight", false);
            status = Status.FAILURE;
            Print(bt);
        }
    }
}
