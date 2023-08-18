using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public static class ReceiverActions
{
    public static Action<string> OnReceivedText;
}

public class TCPReceiver : MonoBehaviour
{
    public int listenPort = 2000;
    private TcpListener listener;
    private TcpClient client;
    private NetworkStream stream;

    void Start()
    {
        listener = new TcpListener(IPAddress.Any, listenPort);
        listener.Start();
        Debug.Log("TCP receiver started");

        // Começa a aceitar conexões
        Task.Run(() => AcceptClient());
    }

    async void AcceptClient()
    {
        client = await listener.AcceptTcpClientAsync();
        stream = client.GetStream();
        Debug.Log("Client accepted");

        // Começa a receber mensagens
        Task.Run(() => ReceiveMessages());
    }

    async void ReceiveMessages()
    {
        byte[] buffer = new byte[1024];
        int bytesRead;

        while (true)
        {
            try
            {
                bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                if (bytesRead == 0)
                {
                    break; // Conexão foi fechada pelo cliente
                }

                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                ReceiverActions.OnReceivedText?.Invoke(message);
            }
            catch (Exception ex)
            {
                Debug.LogError("Exception: " + ex.ToString());
            }
        }
    }

    void OnApplicationQuit()
    {
        stream?.Close();
        client?.Close();
        listener?.Stop();
    }
}
