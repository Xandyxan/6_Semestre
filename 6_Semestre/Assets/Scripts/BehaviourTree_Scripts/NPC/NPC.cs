using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public EnemyNPCProfile profile;
    
    protected BehaviourTree bt;
    public NavMeshAgent npcAgent;
    public Animator npcAnim;
    public PlayerTestBT player; 
    [Tooltip("To make it chose it's own waypoints, add BTSelecionarWaypoints")]
    public List<Transform> waypoints = new List<Transform>();

    protected virtual void Start()
    {
       bt = GetComponent<BehaviourTree>();
       npcAgent = GetComponent<NavMeshAgent>();
       npcAgent.speed = profile.speed;
       npcAnim = GetComponent<Animator>();
    }

    //classe base da qual todos os NPCs derivam
}
