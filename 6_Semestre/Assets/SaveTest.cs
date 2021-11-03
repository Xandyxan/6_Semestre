using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Scriptbale Objects/Inventory System/SaveTest")]
public class SaveTest : ScriptableObject
{
    public string savePath;
    public SaveDataTest saveDataTest;


    // Start is called before the first frame update
    void Start()
    {
        
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
        formatter.Serialize(stream, saveDataTest);
        stream.Close();
    }

    public void Load()
    {
        //Clear();

        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
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
            SaveDataTest newNumber = (SaveDataTest)formatter.Deserialize(stream);

            saveDataTest.numeroTest = newNumber.numeroTest;
            saveDataTest.numeroTest2 = newNumber.numeroTest2;

            stream.Close();
        }
    }
}

[System.Serializable]
public class SaveDataTest
{
    public int numeroTest;
    public int numeroTest2;
}
