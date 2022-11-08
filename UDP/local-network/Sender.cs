using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using System;
 using System.Net;
 using System.Net.Sockets;
 using System.Text;

public class Sender : MonoBehaviour
{
    public int listenPort = 2022;
    public string submask = "192.168.0.255";
    Socket udp;
    IPEndPoint ep;
    
    // Start is called before the first frame update
    void Start()
    {
        udp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        udp.EnableBroadcast = true;
        IPAddress broadcast = IPAddress.Parse(submask); // change to current submask network ip, do not chage only the last 255
        ep = new IPEndPoint(broadcast, listenPort);
        Debug.Log("Message sent to the broadcast address");
        Send();
    }

    void Send() {
        byte[] sendbuf = Encoding.ASCII.GetBytes("Hello World 2");
        udp.SendTo(sendbuf, ep);
    }


    void OnApplicationQuit()
    {
        udp.Close();
    }

}
