using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Scriptbale Objects/Inventory System/Inventory")]

public class InventoryObject : ScriptableObject
{
    [SerializeField] private string _savePath;
    public string savePath { get => _savePath; set => _savePath = value; }
    public ItemDatabaseObject database;
    public Inventory Container;

    public bool AddItem(Item _item, int _amount)
    {
        #region Most recent block
        InventorySlot slot = FindItemOnInventory(_item);

        if (database.Items[_item.Id].stackbable && slot != null)
        {
            slot.ChangeAmount(_amount);
            return true;
        }
        #endregion

        if (EmptySlotCount <= 0)
            return false;

        if(!database.Items[_item.Id].stackbable || slot == null)
        {
            SetEmptySlot(_item, _amount);
            return true;
        }
        slot.ChangeAmount(_amount);
        return true;
    }

    public int EmptySlotCount
    {
        get
        {
            int counter = 0;
            for(int i = 0; i < Container.Items.Length; i++)
            {
                if (Container.Items[i].item.Id <= -1)
                    counter++;
            }

            return counter++;
        }
    }

    public InventorySlot FindItemOnInventory(Item _item)
    {
        for(int i = 0; i < Container.Items.Length; i++)
        {
            if(Container.Items[i].item.Id == _item.Id)
            {
                return Container.Items[i];
            }
        }

        return null;
    }

    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        for(int i = 0; i < Container.Items.Length; i++)
        {
            if(Container.Items[i].item.Id <= -1)
            {
                Container.Items[i].UpdateSlot(_item, _amount);
                return Container.Items[i];
            }
        }
        return null;
    }

    public void SwapItems(InventorySlot item1, InventorySlot item2)
    {
        if(item2.CanPlaceInSlot(item1.itemObject) && item1.CanPlaceInSlot(item2.itemObject))
        {
            InventorySlot temp = new InventorySlot(item2.item, item2.amount);
            item2.UpdateSlot(item1.item, item1.amount);
            item1.UpdateSlot(temp.item, temp.amount);
        }

    }

    public void RemoveItem(Item _item)
    {
        for(int i = 0; i < Container.Items.Length; i++)
        {
            if(Container.Items[i].item ==_item)
            {
                Container.Items[i].UpdateSlot(null, 0);
            }
        }
    }

    
    public void Save()
    {
        #region EDITABLE WAY
        /*
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, _savePath));
        bf.Serialize(file, saveData);
        file.Close();
        */
        #endregion

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }

    public void Load()
    {
        Clear();

        if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            #region EDITABLE WAY
            /*
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, _savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
            */
            #endregion

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);

            for(int i = 0; i < Container.Items.Length; i++)
            {
                Container.Items[i].UpdateSlot(newContainer.Items[i].item, newContainer.Items[i].amount);
            }

            stream.Close();
        }
    }

    public void DeleteSave()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            for (int i = 0; i < Container.Items.Length; i++)
            {
                Container.Items[i].UpdateSlot(null, 0);
            }

            File.Delete(string.Concat(Application.persistentDataPath, savePath));
        }

        Clear();
    }

    public void Clear()
    {
        Container.Clear();
    }

}

[System.Serializable]
public class Inventory
{
    public InventorySlot[] Items = new InventorySlot[24];

    public void Clear()
    {
        for(int i = 0; i < Items.Length; i++)
        {
            Items[i].RemoveItem();
        }
    }
}


[System.Serializable]
public class InventorySlot
{
    public ItemType[] allowedItems = new ItemType[0];
    [System.NonSerialized]
    public UserInterface parent;
    public Item item = new Item();
    public int amount;

    public ItemObject itemObject
    {
        get
        {
            if (item.Id >= 0)
            {
                return parent.inventory.database.Items[item.Id];
            }
            return null;
        }
    }

    //set default values
    public InventorySlot()
    {
        item = new Item();
        amount = 0;
    }

    //constructor
    public InventorySlot(Item _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }
    public void UpdateSlot(Item _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }
    public void RemoveItem()
    {
        item = new Item();
        amount = 0;
    }
    public void ConsumeItem(int _amountToChange)
    {
        ChangeAmount(_amountToChange);
        //acho que aqui faria algo do item
    }

    public void ChangeAmount(int value)
    {
        if (itemObject.stackbable)
        {
            amount += value;
            if (amount <= 0)
            {
                RemoveItem();
            }
        }
        else
        {
            Debug.Log("Operação inválida");
        }
    }
    public bool CanPlaceInSlot(ItemObject _itemObject)
    {
        if (allowedItems.Length <= 0 || _itemObject == null || _itemObject.data.Id < 0) return true;

        for(int i = 0; i < allowedItems.Length; i++)
        {
            if (_itemObject.type == allowedItems[i]) return true;
        }

        return false;
    }

    public int GetItemIDFromSlot()
    {
        return item.Id;
    }
}

