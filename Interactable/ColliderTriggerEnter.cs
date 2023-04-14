using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderTriggerEnter : MonoBehaviour
{
    
    public string CompareTag;

    public UnityEvent TriggerEnter;

    //o outro objeto necessariamente tem que ter um rigigbody para dar certo
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CompareTag))
        {
            TriggerEnter.Invoke();
        }
    }
}
