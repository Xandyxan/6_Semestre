using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEmissionTest : MonoBehaviour
{
    // script made to test how to change the emission intensity of 

    private Material testMaterial;
    private Color materialColor;
    private float emissionIntensity = 1;

    private void Awake()
    {
        testMaterial = GetComponent<Renderer>().material;
        materialColor = testMaterial.GetColor("_EmissionColor");
        print("Material color inicial: " + materialColor);
    }

    private void Start()
    {
        SetIntensity(1.5f);
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ReduceIntensity(3f);
        }else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            AddIntensity(6f);
        }
    }

    private void AddIntensity()
    {
        emissionIntensity *= 2;
        testMaterial.SetColor("_EmissionColor", materialColor * emissionIntensity); // cor já existente + 1

        print("Material color Add: " + materialColor * emissionIntensity);
    }

    private void ReduceIntensity() // reduzir metade representa -1 de intensidade
    {
        emissionIntensity *= 0.5f;
        testMaterial.SetColor("_EmissionColor", materialColor * emissionIntensity); // cor já existente - 1
        print("Material color reduce: " + materialColor * emissionIntensity);
    }

    private void SetIntensity(float intensidade) // funciona até com valor quebrado
    {
        emissionIntensity = Mathf.Pow(2, intensidade);
        testMaterial.SetColor("_EmissionColor", materialColor * emissionIntensity);
    }

    private void ReduceIntensity(float intensidade)
    {
        emissionIntensity *= Mathf.Pow(0.5f, intensidade);
        testMaterial.SetColor("_EmissionColor", materialColor * emissionIntensity);
    }

    private void AddIntensity(float intensidade)
    {
        emissionIntensity *= Mathf.Pow(2, intensidade);
        testMaterial.SetColor("_EmissionColor", materialColor * emissionIntensity);
    }
}
