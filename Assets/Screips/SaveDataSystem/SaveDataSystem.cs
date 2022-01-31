using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
public class SaveDataSystem
{
    private const string _saveFileName = "/playerSaves.saves";
    public static void SaveData(PlayerData playerData)
    {
        var formatter = new BinaryFormatter();
        var path = Application.persistentDataPath + _saveFileName;
        using var fileStream = new FileStream(path, FileMode.Create);
        formatter.Serialize(fileStream, playerData);
        Debug.Log("Data was saved");
    }

    public static PlayerData LoadData()
    {
        var formatter = new BinaryFormatter();
        var path = Application.persistentDataPath + _saveFileName;

        if (File.Exists(path))
        {
            using var fileStream = new FileStream(path, FileMode.Open);
            return formatter.Deserialize(fileStream) as PlayerData;
        }
        else
        {
            Debug.LogError("Save file did not found ");
            return null;
        }

    }
}