using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTNode
{
    public enum Status { RUNNING, FAILURE, SUCCESS }

    public Status status;

    public abstract IEnumerator Run(BehaviourTree bt);

    public void Print(BehaviourTree bt)
    {
        string cor = "cyan";
        if (status == Status.SUCCESS) cor = "green";
        else if (status == Status.FAILURE) cor = "orange";

        string texto = bt.name + " - " + this.GetType().Name + " : " + status.ToString();
        Debug.Log("<color=\"" + cor + "\">" + texto + "</color>");
    }
}
