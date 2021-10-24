using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamparina : MonoBehaviour
{
    // Fazer com que manter manter a lanterna ligada gaste combustivel, o que torna o brilho dela cada vez mais fraco. Caso o combustivel esteja zerado, não permitir
    // que o jogador ative a lamparina

    [SerializeField] private GameObject lamparina;

    [SerializeField] private Light lamparinaLight; // max intensity = 5 -> 0 fuel, 0 intensity. 100 fuel, 5 intensity.

    [SerializeField] private int fuelAmount = 100; // quanto combustivel a lamparina tem

    private bool lightActive = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(fuelAmount > 0)
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
            fuelAmount -= 5;
            if(fuelAmount <= 0)
            {
                fuelAmount = 0;
                lamparina.SetActive(false);
                break;
            }
            UpdateLightIntensity();
            yield return null;
        }
        
    }

    private void UpdateLightIntensity()
    {
        lamparinaLight.intensity = fuelAmount / 20;
    }
}
