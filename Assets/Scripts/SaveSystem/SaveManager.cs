using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    //private string path;

    public static bool Save(string pathName, object data)
    {
        BinaryFormatter formatter = GetBinaryFormatter();
        if (!Directory.Exists(Application.persistentDataPath + "/SaveData"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveData/" + pathName + ".save");
        }

        FileStream file = File.Create(pathName);

        formatter.Serialize(file, data);

        file.Close();

        return true;
    }

    public static object Load(string pathName)
    {
        if (!File.Exists(pathName))
        {
            return null;    
        }

        BinaryFormatter formatter = GetBinaryFormatter();

        FileStream file = File.Open(pathName, FileMode.Open);

        try 
        {
            object save = formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch
        {
            Debug.LogError("No Load File");
            file.Close();
            return null;
        }
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        return formatter;
    }
}
