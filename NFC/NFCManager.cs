using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class NFCManager : MonoBehaviour
{
    
    public Text receivedText;
    public UnityEvent onCafe;
    public UnityEvent onCana;
    public UnityEvent onMilho;
    public UnityEvent onSoja;
    public UnityEvent onTrigo;
    public UnityEvent onMilhoA;
    public UnityEvent onSojaA;

    
    public void SetCafe() {
        PlayerPrefs.SetString("cafe", receivedText.text);
    }
    public void SetCana() {
        PlayerPrefs.SetString("cana", receivedText.text);
    }
    public void SetMilho() {
        PlayerPrefs.SetString("milho", receivedText.text);
    }
    public void SetSoja() {
        PlayerPrefs.SetString("soja", receivedText.text);
    }
    public void SetTrigo() {
        PlayerPrefs.SetString("trigo", receivedText.text);
    }
    public void SetMilhoA() {
        PlayerPrefs.SetString("milhoA", receivedText.text);
    }
    public void SetSojaA() {
        PlayerPrefs.SetString("sojaA", receivedText.text);
    }


    public void OnReceived() {
        string receivedID = receivedText.text;
        if(receivedID == PlayerPrefs.GetString("cafe", "null")) {
            onCafe.Invoke();
        }
        if(receivedID == PlayerPrefs.GetString("cana", "null")) {
            onCana.Invoke();
        }
        if(receivedID == PlayerPrefs.GetString("milho", "null")) {
            onMilho.Invoke();
        }
        if(receivedID == PlayerPrefs.GetString("soja", "null")) {
            onSoja.Invoke();
        }
        if(receivedID == PlayerPrefs.GetString("trigo", "null")) {
            onTrigo.Invoke();
        }
        if(receivedID == PlayerPrefs.GetString("milhoA", "null")) {
            onMilhoA.Invoke();
        }
        if(receivedID == PlayerPrefs.GetString("sojaA", "null")) {
            onSojaA.Invoke();
        }
    }

}
