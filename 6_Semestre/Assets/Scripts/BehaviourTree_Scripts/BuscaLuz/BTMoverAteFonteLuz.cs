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

        GameObject[] lightSources = GameObject.FindGameObjectsWithTag("LightSource");
        GameObject alvo = null;
        float distance = Mathf.Infinity;
        
        foreach(GameObject luzObj in lightSources)
        {
            float dist = Vector3.Distance(luzObj.transform.position, bt.transform.position);
            if(dist < distance)
            {
                alvo = luzObj;
                distance = dist;
            }
        }

        if(alvo!= null)
        {
            while (alvo.activeInHierarchy == true)
            {
                npc.npcAgent.SetDestination(alvo.transform.position);

                if (npc.npcAgent.remainingDistance < 1)
                {
                    status = Status.SUCCESS;
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
