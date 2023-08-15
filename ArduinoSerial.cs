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
        InvokeRepeating("ReadData", .5f, .1f);
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
        OnReceived.Invoke(code.ToString());
        print("byte" + code.ToString());
    }

    public void ReadData()
    {
        stream.DiscardInBuffer();
        string strReceived = stream.ReadLine();
        OnReceived.Invoke(strReceived);
        Debug.Log("string" + strReceived);
    }

    public void SendData(string data)
    {
        if (stream.IsOpen)
        {
            //stream.Write(data);
            byte value;
            if (Byte.TryParse("byte"+data, out value))
            {
                byte[] buffer = new byte[] { value };
                stream.Write(buffer, 0, 1);
            }
            else
            {
                Debug.LogWarning("Failed to convert data to byte: " + data);
            }
            stream.WriteLine("string" + data);
        }
    }
}

// Crie uma nova classe de evento para aceitar o argumento.
[System.Serializable]
public class ReceivedEvent : UnityEvent<string> { }
