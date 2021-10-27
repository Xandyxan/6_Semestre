using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCEnemyProfile", menuName = "NPC/EnemyProfile")]
public class EnemyNPCProfile : ScriptableObject
{
    public float speed = 1.5f;

    public float turningSpeed = 120f;

    public float playerDtRange;

    public float lightSourceDtRange;

    public float atackRange;

    public float waypointSelectionRange;

    public int damage;
}
