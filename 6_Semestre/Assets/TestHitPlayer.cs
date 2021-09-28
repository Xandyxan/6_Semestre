using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHitPlayer : MonoBehaviour
{
    private NPCPaleGhost parentNpc;
    private bool alreadHit = false;

    private void Awake()
    {
        parentNpc = transform.parent.GetComponent<NPCPaleGhost>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!alreadHit)
            {
                PlayerTestBT player = other.GetComponent<PlayerTestBT>();
                if (player) player.TakeDamage(parentNpc.profile.damage);
                alreadHit = true;
                Invoke("SetAlreadHitFalse", .6f);
                CinemachineShake.Instance.ShakeCamera(.4f, .1f);
                parentNpc.gameObject.SetActive(false);
            }
           
        }
    }

    private void SetAlreadHitFalse()
    {
        alreadHit = false;
    }

}
