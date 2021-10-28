using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCry : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.RUNNING;
        Print(bt);
        

        NPCTortuosoGhost npc = bt.GetComponent<NPCTortuosoGhost>();

        if(npc.crySoundPath != "")
        {
            npc.criesText.text = "Buaaaaa..."; 
            //FMODUnity.RuntimeManager.PlayOneShot(npc.crySoundPath, this.transform.position);
            yield return new WaitForSeconds(1);
            npc.criesText.text = "";
            
            status = Status.SUCCESS;
            yield break;
        }

        if (status == Status.RUNNING) status = Status.FAILURE;
        yield return null;
    }
}
