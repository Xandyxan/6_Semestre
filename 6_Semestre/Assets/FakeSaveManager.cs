using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeSaveManager : MonoBehaviour
{
    [SerializeField] private InventoryObject inventoryObject;
    [SerializeField] private InventoryObject equipamentInventory;
    [SerializeField] private InventoryObject documentInventory;
    [SerializeField] private PlayerDataObject playerData;

    [SerializeField] private string inventorySavePath;
    [SerializeField] private string equipamentSavePath;
    [SerializeField] private string documentSavePath;
    [SerializeField] private string playerDataPath;

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
        documentInventory.Save();
        playerData.Save();
    }

    public void ChangeTo()
    {
        inventoryObject.savePath = this.inventorySavePath;
        equipamentInventory.savePath = this.equipamentSavePath;
        documentInventory.savePath = this.documentSavePath;
        playerData.savePath = this.playerDataPath;
    }

    public void Load()
    {
        ChangeTo();
        inventoryObject.Load();
        equipamentInventory.Load();
        documentInventory.Load();
        playerData.Load();
    }

    public void DeleteSave()
    {
        inventoryObject.DeleteSave();
        equipamentInventory.DeleteSave();
        documentInventory.DeleteSave();
        playerData.DeleteSave();
    }
}
