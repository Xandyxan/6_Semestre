using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    Equipament,
    Default
}
public class ItemObject : ScriptableObject
{
    public Sprite uiDisplay;
    public bool stackbable;
    public ItemType type;
    public Item data = new Item();

    [TextArea(15, 20)]
    public string description;

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }

    public virtual void UseItem(PlayerStats playerStats) { }
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id = -1;

    public Item()
    {
        Name = "";
        Id = -1;
    }

    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.data.Id;
    }

    public int GetItemID()
    {
        return Id;
    }
}
