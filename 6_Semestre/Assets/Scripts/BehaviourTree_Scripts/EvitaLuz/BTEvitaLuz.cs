using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTEvitaLuz : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.RUNNING;
        Print(bt);

        NPCPaleGhost npc = bt.GetComponent<NPCPaleGhost>();

        npc.npcAnim.SetTrigger("Light"); // teste

        while (Vector3.Distance(bt.transform.position, npc.lightSource.position) < npc.profile.lightSourceDtRange)
        {
            Vector3 runDirection = bt.transform.position - npc.lightSource.position;

            Vector3 newPos = bt.transform.position + runDirection;

            npc.npcAgent.SetDestination(newPos);

            yield return null;
        }
        if (Vector3.Distance(bt.transform.position, npc.lightSource.position) >= npc.profile.lightSourceDtRange)
        {
            status = Status.SUCCESS;
            yield break;
        }
        if (status == Status.RUNNING) status = Status.FAILURE;
        Print(bt);
    }
}
