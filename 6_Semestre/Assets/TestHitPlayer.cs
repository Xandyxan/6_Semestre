using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHitPlayer : MonoBehaviour
{
    private NPC parentNpc;
    private bool alreadHit = false;

    private void Awake()
    {
        parentNpc = transform.parent.GetComponent<NPC>(); // ver de fazer isso funcionar para outros tipos de NPC
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!alreadHit)
            {
                PlayerTestBT player = other.GetComponent<PlayerTestBT>(); // script que por enquanto lida com as questões de vida do player

                if (player) player.TakeDamage(parentNpc.profile.damage);
                alreadHit = true;
                Invoke("SetAlreadHitFalse", .6f);
                CinemachineShake.Instance.ShakeCamera(.4f, .1f);
            }
           
        }
    }

    private void SetAlreadHitFalse()
    {
        alreadHit = false;
    }

}
