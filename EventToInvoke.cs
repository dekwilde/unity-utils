using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventToInvoke : MonoBehaviour
{

    public UnityEvent ToEvent;
    public UnityEvent onEnable;
    public UnityEvent onStart;
    public bool state = true;
    public UnityEvent onIfEvent;

    
    void OnEnable() {
        onEnable.Invoke();
    }
    void Start() 
    {
        onStart.Invoke();
    }

    public void ToFalse() {
        state = false;
    }

    public void ToTrue() {
        state = true;
    }

    public void IfEvent() {
        if(state) {
            onIfEvent.Invoke();
        }
        
    }

    public void ToInvoke() {
        ToEvent.Invoke();
    }

}
