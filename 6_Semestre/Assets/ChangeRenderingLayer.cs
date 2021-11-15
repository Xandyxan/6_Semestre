using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRenderingLayer : MonoBehaviour
{
    // [SerializeField] private LayerMask layersToHide;
    [SerializeField] private List<string> layersToHide = new List<string>();
    private Camera mainCam;

    [Tooltip("If false, the layer will be rendered by the camera")]
    [SerializeField] private bool hideLayer;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (hideLayer)
            {
                HideLayers();
            }
            else
            {
                ShowLayers();
            }
           
        }
    }

    private void ShowLayers()
    {
        foreach(string layer in layersToHide)
        {
            mainCam.cullingMask |= 1 << LayerMask.NameToLayer(layer);
        }

    }

    private void HideLayers()
    {
        foreach(string layer in layersToHide)
        {
            mainCam.cullingMask &= ~(1 << LayerMask.NameToLayer(layer));
        }
        
    }

}
