using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataObject : MonoBehaviour
{
    public SaveTest saveTest;

    public bool _print, load, save;

    private void Start()
    {
        saveTest.Load();
    }

    private void Update()
    {
        if (load)
        {
            saveTest.Load();
            load = false;
            Debug.Log("Load success");
        }

        if (save)
        {
            saveTest.Save();
            save = false;
            Debug.Log("Save success");
        }

        Debug.Log(saveTest.saveDataTest.numeroTest + " " + saveTest.saveDataTest.numeroTest2);
    }

}
