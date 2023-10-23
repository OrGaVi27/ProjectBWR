using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataChanges : MonoBehaviour
{
    static private string path = Path.Combine(Application.persistentDataPath, "data.json");

    public static void WriteData(DataPersisted data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    public static DataPersisted LoadData()
    {
        if (File.Exists(path))
        {
            string savedJson = File.ReadAllText(path);
            DataPersisted data = JsonUtility.FromJson<DataPersisted>(savedJson);
            return data;
        }
        return null;
    }
}
