using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.UI;

public static class ReceiverActions
{
    public static Action<string> OnReceivedText;
}

public class Receiver : MonoBehaviour
{
    public int listenPort = 2000;
    IPEndPoint ep;
    UdpClient udp;

    void Start()
    {
        ep = new IPEndPoint(IPAddress.Any, listenPort);
        udp = new UdpClient(listenPort);
        Debug.Log("receiver start");
    }

    void Update()
    {
        if (udp.Available != 0)
        {
            Byte[] receivedBytes = udp.Receive(ref ep);
            string returnData = Encoding.ASCII.GetString(receivedBytes);
            ReceiverActions.OnReceivedText?.Invoke(returnData);
        }
    }

    void OnApplicationQuit()
    {
        udp.Close();
    }
}
