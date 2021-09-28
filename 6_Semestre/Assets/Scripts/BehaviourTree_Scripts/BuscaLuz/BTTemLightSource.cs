using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTemLightSource : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        GameObject encontrou =  GameObject.FindWithTag("LightSource");
        if (encontrou) status = Status.SUCCESS; 
        else  status = Status.FAILURE;
        Print(bt);
        yield break;
    }
}
