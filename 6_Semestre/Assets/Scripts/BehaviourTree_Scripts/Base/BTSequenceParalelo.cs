using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSequenceParalelo : BTNode
{
    public List<BTNode> children = new List<BTNode>();

    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.RUNNING;

        Dictionary<BTNode, Coroutine> coroutines = new Dictionary<BTNode, Coroutine>();

       while(status == Status.RUNNING)
        {
            foreach(BTNode node in children)
            {
                if (!coroutines.ContainsKey(node)) coroutines.Add(node, bt.StartCoroutine(node.Run(bt)));
                if (node.status != Status.RUNNING) coroutines.Remove(node);
                if(node.status == Status.FAILURE)
                {
                    status = Status.FAILURE;
                    break;
                }

                yield return null;
            }
            if (status == Status.RUNNING && coroutines.Count == 0) status = Status.SUCCESS;
        }

       foreach(Coroutine coroutina in coroutines.Values)
        {
            bt.StopCoroutine(coroutina);
        }   
    }
}
