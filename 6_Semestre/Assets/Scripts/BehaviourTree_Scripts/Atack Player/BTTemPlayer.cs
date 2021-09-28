using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTemPlayer : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player) status = Status.SUCCESS;

        Print(bt);
        yield break;
    }
}
