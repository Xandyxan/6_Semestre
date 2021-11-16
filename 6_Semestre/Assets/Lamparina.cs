using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamparina : MonoBehaviour
{
    // Fazer com que manter manter a lanterna ligada gaste combustivel, o que torna o brilho dela cada vez mais fraco. Caso o combustivel esteja zerado, não permitir
    // que o jogador ative a lamparina

    [SerializeField] private GameObject lamparina;

    [SerializeField] private Light lamparinaLight; // max intensity = 5 -> 0 fuel, 0 intensity. 100 fuel, 5 intensity.

    private PlayerStats playerStats;

    private bool lightActive = false;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(playerStats.currentFuel > 0)
            {
                lamparina.SetActive(!lamparina.activeInHierarchy);
                if (lamparina.activeInHierarchy) StartCoroutine(HandleFuelUsage());
                else StopCoroutine(HandleFuelUsage());
            }
            else
            {
                print("Out of Juice :(");
            }
           
        }


    }

    private IEnumerator HandleFuelUsage()
    {
        while (lamparina.activeInHierarchy)
        {
            UpdateLightIntensity();
            yield return new WaitForSeconds(2.5f);
            playerStats.currentFuel -= 5;
            if(playerStats.currentFuel <= 0)
            {
                playerStats.currentFuel = 0;
                lamparina.SetActive(false);
                break;
            }
            UpdateLightIntensity();
            yield return null;
        }
        
    }

    private void UpdateLightIntensity()
    {
        lamparinaLight.intensity = playerStats.currentFuel / 20;
    }
}
