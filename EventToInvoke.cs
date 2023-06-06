using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventToInvoke : MonoBehaviour
{
    public UnityEvent ToEvent;
    public UnityEvent onEnable;
    public UnityEvent onStart;
    

    void OnEnable() {
        onEnable.Invoke();
    }
    void Start() 
    {
        onStart.Invoke();
    }

    public void ToInvoke() {
        ToEvent.Invoke();
    }

}
