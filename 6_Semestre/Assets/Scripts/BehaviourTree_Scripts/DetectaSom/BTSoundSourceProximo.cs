using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSoundSourceProximo : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;
        NPC npc = bt.GetComponent<NPC>();

        GameObject[] soundSources = GameObject.FindGameObjectsWithTag("SoundSource");
        foreach(GameObject sound in soundSources)
        {
            if(Vector3.Distance(bt.transform.position, sound.transform.position) < 6){

                status = Status.SUCCESS;
                break;
            }
        }
        Print(bt);
        yield break;
    }
}
