using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTLightSourceProximo : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;
        NPC npc = bt.GetComponent<NPC>();

        GameObject[] lightSorces = GameObject.FindGameObjectsWithTag("LightSource");
        foreach(GameObject light in lightSorces)
        {
            if(Vector3.Distance(bt.transform.position, light.transform.position) < npc.profile.lightSourceDtRange)
            {
                status = Status.SUCCESS;
                break;
            }
        }
        Print(bt);
        yield break;
    }
}
