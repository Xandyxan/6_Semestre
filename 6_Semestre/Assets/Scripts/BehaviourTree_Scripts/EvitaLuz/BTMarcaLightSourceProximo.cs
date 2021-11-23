using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTMarcaLightSourceProximo : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.RUNNING;
        Print(bt);

        NPC npc = bt.GetComponent<NPC>();

        GameObject[] lightSources = GameObject.FindGameObjectsWithTag("LightSource");
        GameObject alvo = null;
        float distance = Mathf.Infinity;

        foreach (GameObject luzObj in lightSources)
        {
            float dist = Vector3.Distance(luzObj.transform.position, bt.transform.position);
            if (dist < distance && dist < npc.profile.lightSourceDtRange)
            {
                alvo = luzObj;
                distance = dist;
            }
        }

        if (alvo)
        {
            npc.lightSource = alvo.transform;
            status = Status.SUCCESS;
            npc.npcAnim.SetBool("IsWalking", false);
            Print(bt);
            yield break;
        }

        if (status == Status.RUNNING)
        {
            status = Status.FAILURE;
            Print(bt);
        }
    }
}
