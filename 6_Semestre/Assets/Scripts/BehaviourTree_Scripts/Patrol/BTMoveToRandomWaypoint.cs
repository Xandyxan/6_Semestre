using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTMoveToRandomWaypoint : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.RUNNING;
        NPC npc = bt.GetComponent<NPC>();

        int tamanhoList = npc.waypoints.Count;

        if(tamanhoList > 0)
        {
            int randomIndex = Random.Range(0, tamanhoList);

            while (npc.waypoints[randomIndex])
            {
                npc.npcAgent.SetDestination(npc.waypoints[randomIndex].position);
                npc.npcAnim.SetBool("IsWalking", true);
                if (npc.npcAgent.remainingDistance <= npc.npcAgent.stoppingDistance)
                {
                    status = Status.SUCCESS;
                    npc.npcAnim.SetBool("IsWalking", false);
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
