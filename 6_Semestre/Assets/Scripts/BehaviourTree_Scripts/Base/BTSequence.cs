using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSequence : BTNode
{
    public List<BTNode> children = new List<BTNode>();

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.RUNNING;

        foreach(BTNode node in children)
        {
            yield return bt.StartCoroutine(node.Run(bt));
            if(node.status == Status.FAILURE)
            {
                status = Status.FAILURE;
                yield break;
            }
        }

        status = Status.SUCCESS;
    }
}
