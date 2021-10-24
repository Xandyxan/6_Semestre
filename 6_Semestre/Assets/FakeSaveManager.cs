using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeSaveManager : MonoBehaviour
{
    [SerializeField] private InventoryObject inventoryObject;
    [SerializeField] private InventoryObject equipamentInventory;
    [SerializeField] private string inventorySavePath;
    [SerializeField] private string equipamentSavePath;

    private void Awake()
    {
    }

    private void Start()
    {
    }

    public void Save()
    {
        ChangeTo();
        inventoryObject.Save();
        equipamentInventory.Save();
    }

    public void ChangeTo()
    {
        inventoryObject.savePath = this.inventorySavePath;
        equipamentInventory.savePath = this.equipamentSavePath;
    }

    public void Load()
    {
        ChangeTo();
        inventoryObject.Load();
        equipamentInventory.Load();
    }

    public void DeleteSave()
    {
        inventoryObject.DeleteSave();
        equipamentInventory.DeleteSave();
    }
}
