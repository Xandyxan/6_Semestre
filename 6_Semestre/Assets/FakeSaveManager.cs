using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeSaveManager : MonoBehaviour
{
    [SerializeField] private InventoryObject inventoryObject;
    [SerializeField] private string savePath;
    private bool objectExists;

    private void Awake()
    {
        objectExists = false;
    }

    private void Start()
    {
        if (inventoryObject != null) objectExists = true;
        else objectExists = false;
    }

    public void Save()
    {
        inventoryObject.Save();
    }

    public void ChangeTo()
    {
        inventoryObject.savePath = this.savePath;
    }

    public void Load()
    {
        ChangeTo();
        inventoryObject.Load();    
    }

    public void DeleteSave()
    {
        inventoryObject.DeleteSave();
    }
}
