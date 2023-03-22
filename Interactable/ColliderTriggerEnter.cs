using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderTriggerEnter : MonoBehaviour
{
    
    public string CompareTag;

    public UnityEvent TriggerEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CompareTag))
        {
            TriggerEnter.Invoke();
        }
    }
}
