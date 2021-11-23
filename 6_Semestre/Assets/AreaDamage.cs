using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : MonoBehaviour
{
    // tem um trigger numa sala que contém um fantasma obscuro. Caso o player permaneça nessa sala, ele irá tomar dano de sanidade, que é aumentado caso o player se aproxime da criatura. 
    // On trigger enter marca o player, on exit ref do player = null. 
    // Caso tenha a ref do player, causa X de dano a cada X segundos, podendo escalonar de acordo com a distancia entre o player e o fantasma da sala

    private float distanceFromCreature;
    private PlayerStats playerstatus; // ref do player
    [SerializeField] private Transform creatureTransform; // o fantasma

    private IEnumerator DealAreaDamage()
    {
       while (playerstatus != null && playerstatus.gameObject.activeInHierarchy)
       {
            distanceFromCreature = Vector3.Distance(creatureTransform.position, playerstatus.transform.position);
            float DamageMultiplier = (1 / distanceFromCreature);
            playerstatus.TakeDamage(3 * DamageMultiplier);
            //Damage = 3 * DamageMultiplier;

           yield return new WaitForSeconds(1f);
       }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(playerstatus == null)
        {
            playerstatus = other.GetComponent<PlayerStats>();
        }
        if (playerstatus != null)
        {
            StartCoroutine(DealAreaDamage());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && playerstatus != null)
        {
            StopCoroutine(DealAreaDamage());
            playerstatus = null;
        }
    }
}
