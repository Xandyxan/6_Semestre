using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTDeactivateNPC : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.FAILURE;

        yield return new WaitForSeconds(3f);
        bt.gameObject.SetActive(false);
        status = Status.SUCCESS;

        yield return null;
    }
}
