using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Data")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    //this one will never be serialized
    public ItemObject[] Items;        //GetID

    //will always be serialized losing all content in (OnBeforeDeserialize())
    //public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();  //set the Item from each ID - GetItem

    public void UpdateID()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if((Items[i].data.Id != i))
                Items[i].data.Id = i;
        }
    }
    public void OnAfterDeserialize()
    {
        UpdateID();
    }

    public void OnBeforeSerialize()
    {
        //avoid to duplicate the Dictionary after deserialization
        //GetItem = new Dictionary<int, ItemObject>();

    }

    private void OnEnable()
    {    
        //for (int i = 0; i < Items.Length; i++)   
        //{
        //    Debug.Log("Nome: " + itemObjects[i].name + " (" + itemObjects[i].type + "); " + "ID: "+ i);
        //}
    }
}
