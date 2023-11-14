using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataChanges : MonoBehaviour
{
    //private string path;

    //private void Start()
    //{
    //    path = Path.Combine(Application.persistentDataPath, "data.json");
    //}
    static public void WriteData(DataPersisted data)
    {
        string path = Path.Combine(Application.persistentDataPath, "data.json");
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    static public DataPersisted LoadData()
    {
        string path = Path.Combine(Application.persistentDataPath, "data.json");
        if (File.Exists(path))
        {
            string savedJson = File.ReadAllText(path);
            DataPersisted data = JsonUtility.FromJson<DataPersisted>(savedJson);
            return data;
        }
        return null;
    }
}
