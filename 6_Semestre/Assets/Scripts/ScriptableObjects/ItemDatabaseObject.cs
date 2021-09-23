using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Data")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    //this one will never be serialized
    public ItemObject[] itemObjects;

    //will always be serialized losing all content in (OnAfterDeserialize())
    public Dictionary<ItemObject, int> idDictionary = new Dictionary<ItemObject, int>();    //set the ID of each ItemObject stored in the array
    public Dictionary<int, ItemObject> itemDictionary = new Dictionary<int, ItemObject>();  //set the Item from each ID

    public void OnAfterDeserialize()
    {

        //avoid to duplicate the Dictionary after deserialization
        idDictionary = new Dictionary<ItemObject, int>();
        itemDictionary = new Dictionary<int, ItemObject>();

        for(int i = 0; i < itemObjects.Length; i++)   //repopulate all the 'GetId' dictionary by adding object by object from ItemObject Array (Itens Data)
        {
            idDictionary.Add(itemObjects[i], i);
            itemDictionary.Add(i, itemObjects[i]);
        }
    }

    public void OnBeforeSerialize()
    {

    }

    private void OnEnable()
    {    
        for (int i = 0; i < itemObjects.Length; i++)   
        {
            Debug.Log("Nome: " + itemObjects[i].name + " (" + itemObjects[i].type + "); " + "ID: "+ i);
        }
    }
}
