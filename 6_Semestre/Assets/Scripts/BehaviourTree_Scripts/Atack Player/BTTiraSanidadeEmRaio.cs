using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTiraSanidadeEmRaio : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.RUNNING;
        Print(bt);

        NPC npc = bt.GetComponent<NPC>();
        if (npc.player)
        {
            Ray raio = new Ray(bt.transform.position, npc.player.transform.position - bt.transform.position);
            Debug.DrawRay(bt.transform.position, npc.player.transform.position - bt.transform.position, Color.magenta);
            RaycastHit hit;
            if (Physics.Raycast(raio, out hit, npc.profile.atackRange))
            {
                if (hit.transform == npc.player.transform)
                {
                    int distMultiplier = (int)Vector3.Distance(bt.transform.position, npc.player.transform.position);
                    if (distMultiplier <= 0) distMultiplier = 1;
                    int damage = npc.profile.damage / distMultiplier;
                    npc.player.TakeDamage(damage);
                    status = Status.SUCCESS;
                    Print(bt);
                    yield break;
                }
            }

            if (status == Status.RUNNING) status = Status.FAILURE;
            Print(bt);
        }
    }
}
