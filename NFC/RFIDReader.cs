using Lando;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RFIDReader : MonoBehaviour
{
    private Cardreader cardreader = new Cardreader();
    public CardIdEvent OnReceived; // Crie um novo tipo de evento que aceite argumentos.

    private void Start()
    {
        cardreader.StartWatch();
        cardreader.CardConnected += CardreaderOnCardConnectedHandler;
        cardreader.CardDisconnected += CardreaderOnCardDisconnectedHandler;
        cardreader.CardreaderConnected += CardreaderOnCardreaderConnectedHandler;
        cardreader.CardreaderDisconnected += CardreaderOnCardreaderDisconnectedHandler;
    }

    private void CardreaderOnCardreaderDisconnectedHandler(object sender, CardreaderEventArgs e)
    {
        Debug.Log($"card reader: disconnected");
    }

    private void CardreaderOnCardreaderConnectedHandler(object sender, CardreaderEventArgs e)
    {
        Debug.Log($"card reader: {e.CardreaderName} connected");
    }

    private void CardreaderOnCardDisconnectedHandler(object sender, CardreaderEventArgs e)
    {
        Debug.Log($"card: disconnected");
    }

    private void CardreaderOnCardConnectedHandler(object sender, CardreaderEventArgs e)
    {
        Debug.Log($"card: {e.Card.Id} connected");
        OnReceived.Invoke(e.Card.Id);
    }

    public void DebugReceived() {
        OnReceived.Invoke("hello word");
    }

    private void OnApplicationQuit()
    {
        cardreader.StopWatch();
        cardreader.CardConnected -= CardreaderOnCardConnectedHandler;
        cardreader.CardDisconnected -= CardreaderOnCardDisconnectedHandler;
        cardreader.CardreaderConnected -= CardreaderOnCardreaderConnectedHandler;
        cardreader.CardreaderDisconnected -= CardreaderOnCardreaderDisconnectedHandler;
    }
}

// Crie uma nova classe de evento para aceitar o argumento.
[System.Serializable]
public class CardIdEvent : UnityEvent<string> { }
