using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTemWaypoints : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        GameObject encontrou = GameObject.FindWithTag("Waypoint");
        if (encontrou) status = Status.SUCCESS;
        else status = Status.FAILURE;
        Print(bt);
        yield break;
    }
}
