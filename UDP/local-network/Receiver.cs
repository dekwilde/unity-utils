using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.UI;

public class Receiver : MonoBehaviour
{
    // Start is called before the first frame update

    public const int listenPort = 2022;
    //Socket udp;
    IPEndPoint  ep;
    IPEndPoint  serverEP;
    UdpClient udp;

    public Text Status;

    void Start()
    {
        //ep = new IPEndPoint(IPAddress.Parse(serverIP), port);
        ep = new IPEndPoint(IPAddress.Any, listenPort);
        //udp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        udp = new UdpClient(listenPort);
        udp.EnableBroadcast = true;

        Debug.Log("receiver start");
        Status.text = "receiver start";

    }

    void Update()
    {
        if(udp.Available != 0)
        {
            Byte[] receivedBytes = udp.Receive(ref ep);
            string returnData = Encoding.ASCII.GetString(receivedBytes);
            Debug.Log("Received: " + returnData);
            Status.text = "Received: " + returnData;
        }
    }

    void OnApplicationQuit()
    {
        udp.Close();
    }
    
}
