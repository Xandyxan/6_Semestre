using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTemPlayer : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;

        PlayerStats playerStats = GameObject.FindObjectOfType<PlayerStats>();
        if (playerStats) status = Status.SUCCESS;

        Print(bt);
        yield break;
    }
}
