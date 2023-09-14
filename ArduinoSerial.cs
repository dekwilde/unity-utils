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
            var loadedData = JsonUtility.FromJson<DataJson>(dataAsJson);
            comPort = loadedData.COM;
            StartStream();
        }
    }

    void StartStream()
    {
        stream = new SerialPort(comPort, 9600);
        stream.Open(); //Open the Serial Stream.
        stream.ReadTimeout = 1;
    }

    void Update()
    {
        if (stream.IsOpen)
        {
            try
            {
                ReadDataInt(stream.ReadByte());
            }
            catch (System.Exception) { }
        }
    }

    public void ReadDataInt(int code)
    {
        char receivedChar = (char)code;
        string receivedString = receivedChar.ToString();
        OnReceived.Invoke(receivedString);
        Debug.Log("ReadDataInt: " + receivedString);
    }

    public void SendData(string data)
    {
        if (stream.IsOpen)
        {
            stream.WriteLine(data);
        } else {
            StartStream();
        }
        Debug.Log("SendData " + data);
    }
}

// Crie uma nova classe de evento para aceitar o argumento.
[System.Serializable]
public class ReceivedEvent : UnityEvent<string> { }
