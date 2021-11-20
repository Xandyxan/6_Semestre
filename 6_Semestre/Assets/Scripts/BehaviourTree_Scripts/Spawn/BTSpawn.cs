using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSpawn : BTNode
{
    // apenas um tempo de delay antes de rolar os outros behaviours. Só roda enquanto a bool spawned for false (primeira vez que o npc for spawnado)
    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;

        NPC npc = bt.GetComponent<NPC>();

        if (!npc.spawned)
        {
            yield return new WaitForSeconds(1f);
            status = Status.SUCCESS;
            npc.spawned = true;
        }
        Print(bt);
        yield return null;
    }
}
