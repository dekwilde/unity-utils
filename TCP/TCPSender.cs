using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class TCPSender : MonoBehaviour
{
    public string serverIP = "127.0.0.1"; // Endereço IP do servidor ao qual deseja se conectar
    public int port = 2000; // Porta para a conexão TCP

    private TcpClient client;
    private NetworkStream stream;

    void Start()
    {
        // Inicia a conexão TCP
        client = new TcpClient();
        try
        {
            client.Connect(IPAddress.Parse(serverIP), port); // Conecta ao servidor
            stream = client.GetStream(); // Obtem o stream de dados
            Debug.Log("TCP sender connected");
        }
        catch (SocketException se)
        {
            Debug.LogError("SocketException: " + se.ToString());
        }
    }

    public void Send(string message)
    {
        if (client == null || !client.Connected)
        {
            Debug.LogError("Not connected to the server.");
            return;
        }

        try
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length); // Envia a mensagem ao servidor
            Debug.Log("Sent: " + message);
        }
        catch (Exception e)
        {
            Debug.LogError("Exception: " + e.ToString());
        }
    }

    void OnApplicationQuit()
    {
        if (stream != null)
        {
            stream.Close();
        }
        if (client != null)
        {
            client.Close();
        }
    }
}
