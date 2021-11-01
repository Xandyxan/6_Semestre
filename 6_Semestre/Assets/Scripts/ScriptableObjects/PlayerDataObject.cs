using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "Scriptbale Objects/PlayerData")]
public class PlayerDataObject : ScriptableObject
{
    //Backing Variables
    [SerializeField] private string _savePath;

    public string savePath { get => _savePath; set => _savePath = value; }
    public PlayerData playerData;

    public void Save()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, playerData);
        stream.Close();
    }

    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            PlayerData newContainer = (PlayerData)formatter.Deserialize(stream);

            playerData.health = newContainer.health;
            playerData.lamparinaFuel = newContainer.lamparinaFuel;

            stream.Close();
        }
    }

    public void DeleteSave()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            File.Delete(string.Concat(Application.persistentDataPath, savePath));
        }
    }
}

[System.Serializable]
public class PlayerData
{
    public float health;
    public float lamparinaFuel;
}
