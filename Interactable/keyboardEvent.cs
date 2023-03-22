using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class keyboardEvent : MonoBehaviour
{



    public UnityEvent Key1;
    public UnityEvent Key2;
    public UnityEvent Key3;
    public UnityEvent Key4;
    public UnityEvent KeyA;
    public UnityEvent KeyW;
    public UnityEvent KeyS;
    public UnityEvent KeyD;
    public UnityEvent KeyR;
    public UnityEvent KeyP;

    
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
    }
}
