using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public static class SaveSystem 
{
    public static Action OnSave;
    public static void CreateSaveData(Player player, Inventory inventory)
    {
        OnSave?.Invoke();
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/SaveData.fun";
        FileStream stream = new FileStream(path, FileMode.Create);


        SaveData data = new SaveData(player, inventory);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadSaveData()
    {
        string path = Application.persistentDataPath + "/SaveData.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path) ;
            return null;
        }
    }
}
