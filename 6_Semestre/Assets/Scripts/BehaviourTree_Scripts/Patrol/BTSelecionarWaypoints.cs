using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSelecionarWaypoints : BTNode
{
    public override IEnumerator Run(BehaviourTree bt)
    {
        status = Status.RUNNING;

        NPC npc = bt.GetComponent<NPC>();
        GameObject[] waypointsObj = GameObject.FindGameObjectsWithTag("Waypoint");

        npc.waypoints.Clear(); 
        foreach(GameObject waypointObj in waypointsObj)
        {
            if (Vector3.Distance(bt.transform.position, waypointObj.transform.position) < npc.profile.waypointSelectionRange)
            {
                npc.waypoints.Add(waypointObj.transform);
            }
            else continue;
        }

        if (npc.waypoints.Count > 0) status = Status.SUCCESS;
        else status = Status.FAILURE;
        Print(bt);
        yield break;
    }
}
