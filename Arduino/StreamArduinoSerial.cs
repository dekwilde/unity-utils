using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.IO;
using System.IO.Ports;


public class StreamArduinoSerial : MonoBehaviour
{
    SerialPort stream;
    string strReceived;
    [SerializeField] private TextMeshProUGUI arduinoUIText;
    private string comPort;

    public gameManager manager;

    private void Awake()
    {
       loadJSON();
    }

    // a configuração da porta COM é feita pelo json
    private void loadJSON()
    {
        string filePath = Path.Combine (Application.streamingAssetsPath + "/", "ipconfig.json");
        if (File.Exists (filePath)) {
            var dataAsJson = File.ReadAllText(filePath);
            var loadedData = JsonUtility.FromJson<jsonData> (dataAsJson);
            comPort = loadedData.pcPort;
            StartStream();
        } 
    }

    void StartStream()
    {
        stream = new SerialPort(comPort, 9600);
        stream.Open(); //Open the Serial Stream.
        InvokeRepeating("ReadLine", .5f, .1f);
    }

    public void ReadLine()
    {
        stream.DiscardInBuffer();
        strReceived = stream.ReadLine();
        Debug.Log($"{strReceived}");
        manager.onReceive(strReceived);

    }

}



