using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamparina : MonoBehaviour
{
    // Fazer com que manter manter a lanterna ligada gaste combustivel, o que torna o brilho dela cada vez mais fraco. Caso o combustivel esteja zerado, não permitir
    // que o jogador ative a lamparina
    private PlayerStats playerStats;

    [Header("LightSource")]
    [SerializeField] private GameObject lamparina;
    [SerializeField] private Light lamparinaLight; // max intensity = 5 -> 0 fuel, 0 intensity. 100 fuel, 5 intensity.
    private bool lightActive = false;
    [SerializeField] private GameObject lightSource;

    [Header("Emission")]
    private Material lamparinaGlassMaterial; // max emission = 5 -> 0 fuel, 0 intensity. 100 fuel, 5 intensity. Ativar emission junto à luz.
    private Color emissionColor;
    private float emissionIntensity = 1;

    [Header("Fuel")]
    //[SerializeField] private int fuelUsage = 1; // quanto de combustivel é gasto a cada ciclo
    [SerializeField] private float fuelUnitLifeTime = 2;
    private bool lamparinaOn = false;

    private void Awake()
    {
        lamparinaGlassMaterial = lamparina.GetComponent<Renderer>().materials[1];
        emissionColor = lamparinaGlassMaterial.GetColor("_EmissionColor");
        playerStats = GetComponent<PlayerStats>();
    }

    private void Start()
    {
        lamparina.SetActive(false);
        lightSource.SetActive(false);
        SetIntensity(5);
        lamparinaGlassMaterial.DisableKeyword("_EMISSION");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (playerStats.currentFuel > 0)
            {
                lamparinaOn = !lamparinaOn;
                if (lamparinaOn)
                {
                    lightSource.SetActive(true);
                    lamparinaGlassMaterial.EnableKeyword("_EMISSION");
                    StartCoroutine(HandleFuelUsage());
                }
                else
                {
                    StopCoroutine(HandleFuelUsage());
                    lightSource.SetActive(false);
                    lamparinaGlassMaterial.DisableKeyword("_EMISSION");
                }        
            }
            else
            {
                playerStats.currentFuel = 0;
            }  
        }
    }

    private IEnumerator HandleFuelUsage()
    {
        while (lamparina.activeInHierarchy)
        {
            //lamparinaGlassMaterial.EnableKeyword("_EMISSION");
            UpdateLightIntensity();
            yield return new WaitForSeconds(fuelUnitLifeTime);
            playerStats.currentFuel -= 1;

            Debug.LogWarning("Tá gastando");

            if(playerStats.currentFuel <= 0)
            {
                playerStats.currentFuel = 0;
                lightSource.SetActive(false);
                lamparinaGlassMaterial.DisableKeyword("_EMISSION");
                break;
            }

            UpdateLightIntensity();
            yield return null;
        }
    }

    private void UpdateLightIntensity()
    {
        float intensityvalue = playerStats.currentFuel / 20f; // começa em 5 e subtrai 0.05f para cada 1 de fuel perdido.
        if (intensityvalue <= 1) intensityvalue = 1;

        lamparinaLight.intensity = intensityvalue;
        SetIntensity(intensityvalue + 1f);
       
    }

    // Mudar intensidade Emission \\
    private void AddIntensity()
    {
        emissionIntensity *= 2;
        lamparinaGlassMaterial.SetColor("_EmissionColor", emissionColor * emissionIntensity); // cor já existente + 1
    }

    private void ReduceIntensity() // reduzir metade representa -1 de intensidade
    {
        emissionIntensity *= 0.5f;
        lamparinaGlassMaterial.SetColor("_EmissionColor", emissionColor * emissionIntensity); // cor já existente - 1
    }

    private void SetIntensity(float intensidade) // funciona até com valor quebrado
    {
        emissionIntensity = Mathf.Pow(2, intensidade);
        lamparinaGlassMaterial.SetColor("_EmissionColor", emissionColor * emissionIntensity);
    }

    private void ReduceIntensity(float intensidade)
    {
        emissionIntensity *= Mathf.Pow(0.5f, intensidade);
        lamparinaGlassMaterial.SetColor("_EmissionColor", emissionColor * emissionIntensity);
    }

    private void AddIntensity(float intensidade)
    {
        emissionIntensity *= Mathf.Pow(2, intensidade);
        lamparinaGlassMaterial.SetColor("_EmissionColor", emissionColor * emissionIntensity);
    }
}
