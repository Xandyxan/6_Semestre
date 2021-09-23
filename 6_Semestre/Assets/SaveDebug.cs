using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveDebug : MonoBehaviour
{
    [SerializeField] private InventoryObject inventoryObject;
    private bool objectExists;

    [Header("UI Elements")]
    [SerializeField] private Text currentSavePath;


    private void Awake()
    {
        objectExists = false;
    }

    private void Start()
    {
        if (inventoryObject != null) objectExists = true;
        else objectExists = false;
    }

    private void Update()
    {
        if(objectExists)
        {
            currentSavePath.text = inventoryObject.savePath;
        }
    }

}
