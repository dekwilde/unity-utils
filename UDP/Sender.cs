using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Sender : MonoBehaviour
{
    public int listenPort = 2000;
    public string submask = "192.168.0.255";
    Socket udp;
    IPEndPoint ep;

    void Start()
    {
        udp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        udp.EnableBroadcast = true;
        IPAddress broadcast = IPAddress.Parse(submask); // change to current submask network ip, do not chage only the last 255
        ep = new IPEndPoint(broadcast, listenPort);
        Debug.Log("sender start");
    }

    public void Send(string str)
    {
        byte[] sendbuf = Encoding.ASCII.GetBytes(str);
        udp.SendTo(sendbuf, ep);
    }

    void OnApplicationQuit()
    {
        udp.Close();
    }
}
