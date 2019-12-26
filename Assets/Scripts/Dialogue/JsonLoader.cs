using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public class JsonLoader
{
    private string m_FilePath;

    public JsonLoader(string folderName, string fileName)
    {
        m_FilePath = Application.streamingAssetsPath + "/" + folderName + "/" + fileName + ".json";
    }

    public List<T> LoadList<T>()
    {
        if (File.Exists(m_FilePath))
        {
            string dataAsJson = File.ReadAllText(m_FilePath);
            List<T> listItems = JArray.Parse(dataAsJson).ToObject<List<T>>(); 

            return listItems;
        }
        else
        {
            Debug.Log("Path not found");
            return null;
        }
    }
}
