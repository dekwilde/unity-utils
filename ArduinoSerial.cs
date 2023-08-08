using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using System.IO;
using System.IO.Ports;
using UnityEngine.Events;

public class ArduinoSerial : MonoBehaviour
{
    SerialPort stream;
    string strReceived;
    string comPort;
    public ReceivedEvent OnReceived;

    private void Awake()
    {
        loadJSON();
    }

    // a configuração da porta COM é feita pelo json
    private void loadJSON()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath + "/", "config.json");
        if (File.Exists(filePath))
        {
            var dataAsJson = File.ReadAllText(filePath);
            var loadedData = JsonUtility.FromJson<JsonData>(dataAsJson);
            comPort = loadedData.COM;
            StartStream();
        }
    }

    void StartStream()
    {
        stream = new SerialPort(comPort, 9600);
        stream.Open(); //Open the Serial Stream.
        InvokeRepeating("ReadData", .5f, .1f);
    }

    public void ReadData()
    {
        stream.DiscardInBuffer();
        string strReceived = stream.ReadLine();
        Debug.Log($"{strReceived}");
        OnReceived.Invoke(strReceived);
    }

    public void SendData(string data)
    {
        if (stream.IsOpen)
        {
            stream.Write(data);
        }
    }
}

// Crie uma nova classe de evento para aceitar o argumento.
[System.Serializable]
public class ReceivedEvent : UnityEvent<string> { }
