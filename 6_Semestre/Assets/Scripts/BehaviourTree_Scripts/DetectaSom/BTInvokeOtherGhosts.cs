using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTInvokeOtherGhosts : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;
        
        NPCTortuosoGhost npc = bt.GetComponent<NPCTortuosoGhost>();

        npc.npcAnim.SetTrigger("Call"); // roda a animação do fantasma gritando
        npc.criesText.text = "AAAAAAAAAAA";
        yield return new WaitForSeconds(1.5f);

        if(npc.ghosts.Length > 0) // se tiverem fantasmas no array de fantasmas desse tortuoso, invoca eles
        {
            foreach(GameObject ghost in npc.ghosts) 
            {
                // sortear posição de spawn dos outros fantasmas com base em waypoints pré definidos
                ghost.SetActive(true);
                yield return null; 
            }
            npc.criesText.text = "";
            status = Status.SUCCESS;
            Print(bt);
            yield break;
        }

        //if (status == Status.RUNNING) status = Status.FAILURE;
        Print(bt);
        yield return null;
    }
}
