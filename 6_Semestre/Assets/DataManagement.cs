using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManagement : MonoBehaviour
{
    [SerializeField] private InventoryObject playerInventory;
    [SerializeField] private InventoryObject equipamentInventory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationQuit()
    {
        playerInventory.Container.Clear();
        equipamentInventory.Container.Clear();
    }
}
