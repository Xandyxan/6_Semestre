using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : MonoBehaviour
{
    // tem um trigger. On trigger enter marca o player, on exit ref do player = null. Caso tenha a ref do player, causa 0.5 de dano a cada segundo, podendo escalonar de acordo com a dist.

    private PlayerStats playerstatus;

    private void OnTriggerEnter(Collider other)
    {
        if(playerstatus == null)
        {
            playerstatus = other.GetComponent<PlayerStats>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && playerstatus != null)
        {
            playerstatus = null;
        }
    }
}
