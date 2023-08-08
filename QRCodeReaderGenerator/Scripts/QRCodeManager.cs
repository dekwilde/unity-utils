using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class QRCodeManager : MonoBehaviour
{
    public string baseUrl;
    public GenerateEvent OnGenerate;

    void Start()
    {
        baseUrl = loadJSON();
    }

    public void qrcodeGenerate(string filename)
    {
        string url = baseUrl + filename;
        OnGenerate.Invoke(url);
    }

    string loadJSON()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath + "/", "config.json");
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            JsonData loadedData = JsonUtility.FromJson<JsonData>(dataAsJson);
            Debug.Log("loadJSON base_url " + loadedData.base_url);
            return loadedData.base_url;
        }
        else
        {
            return "https://postal.social/niemeyerlinhaseuzes-202309/?";
            Debug.LogError("Cannot find file! named");
        }
    }
}

[System.Serializable]
public class GenerateEvent : UnityEvent<string> { }
