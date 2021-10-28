using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTActivateHitBox : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.RUNNING;
        Print(bt);

        NPC npc = bt.GetComponent<NPC>();
        if (npc.player)
        {
            if(Vector3.Distance(npc.transform.position, npc.player.transform.position) < npc.profile.atackRange)
            {
                npc.transform.LookAt(npc.player.transform);

                if (npc.npcAnim)
                {
                    npc.npcAnim.SetTrigger("Attack");

                    yield return new WaitForSeconds(.8f);

                    status = Status.SUCCESS;
                }
               
            }
        }
     
        if(status == Status.RUNNING) status = Status.FAILURE;
        Print(bt);
        yield break;
    }
}
