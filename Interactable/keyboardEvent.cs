using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardEvent : MonoBehaviour
{
    public UnityEvent Key1;
    public UnityEvent Key2;
    public UnityEvent Key3;
    public UnityEvent Key4;
    public UnityEvent Key5;
    public UnityEvent Key6;
    public UnityEvent KeyA;
    public UnityEvent KeyW;
    public UnityEvent KeyS;
    public UnityEvent KeyD;
    public UnityEvent KeyR;
    public UnityEvent KeyP;
    public UnityEvent KeySpace;  // Adicionando evento para a tecla Espaço

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Key1.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Key2.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Key3.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Key4.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Key5.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Key6.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            KeyA.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            KeyW.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            KeyS.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            KeyD.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            KeyR.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            KeyP.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Space))  // Verificando se a tecla Espaço foi pressionada
        {
            KeySpace.Invoke();
        }
    }
}
