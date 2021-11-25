using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGetEmissionColor : MonoBehaviour
{
    [SerializeField] private Material testMaterial;
    private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        testMaterial = rend.material;
        print("Emission color: " + testMaterial.GetColor("_EmissionColor"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
