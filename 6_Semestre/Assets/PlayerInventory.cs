using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private InventoryObject playerInventory;

    public void CollectItem(ItemObject _item)
    {
        playerInventory.AddItem(_item, 1);
    }

    private void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerInventory.Save();
        }

        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            playerInventory.Load();
        }
        */
    }

    private void OnApplicationQuit()
    {
        playerInventory.Container.Clear();
    }
}
