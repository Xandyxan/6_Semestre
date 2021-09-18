using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PaleGhost : MonoBehaviour
{
    // AI keeps patrolling. If the player is close enought, chase them. If theres light, avoid it.
    [Header("Agent Properties")]
    private NavMeshAgent agent;

    [Header("Face the player")]
    [SerializeField] private Transform playerTransform; // used to tell the enemy what to follow
    [Tooltip("The speed wich this will rotate towards the player direction")]
    [SerializeField] private float lookRotationSpeed;
    [Space]
    [Tooltip("How many seconds between each destination update?")]
    [SerializeField] private float destinationRefreshDelay = 2.0f;
    private float destinationRefreshTimer = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
   
    // Update is called once per frame
    void Update()
    {
        if(destinationRefreshTimer <= Time.time)
        {
            agent.destination = playerTransform.position;
           
            destinationRefreshTimer = Time.time + destinationRefreshDelay;
        }
        if (agent.remainingDistance <= 5f)
        {
            LookAtTarget();
        }
       
    }

    private void LookAtTarget()
    {
        Vector3 directionToFace = (playerTransform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(directionToFace.x, 0, directionToFace.z)); // zera o y pra ele só rotacionar lateralmente
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * lookRotationSpeed);
    }
}
