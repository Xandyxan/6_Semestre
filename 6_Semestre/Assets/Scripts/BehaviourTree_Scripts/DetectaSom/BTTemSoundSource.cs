using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTemSoundSource : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        GameObject encontrou = GameObject.FindGameObjectWithTag("SoundSource"); // talvez trocar por um find fmod emitter ou coisa do tipo.

        if (encontrou) status = Status.SUCCESS;
        else status = Status.FAILURE;
        Print(bt);
        yield break;
    }
}
