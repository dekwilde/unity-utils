using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderTrigger : MonoBehaviour
{
    public string CompareTag;

    public UnityEvent TriggerEnter;
    public UnityEvent TriggerExit;   // Evento para quando sair do Trigger
    public UnityEvent CollisionEnter;
    public UnityEvent CollisionExit; // Evento para quando sair da colisão

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CompareTag))
        {
            TriggerEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(CompareTag))
        {
            TriggerExit.Invoke();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(CompareTag))
        {
            CollisionEnter.Invoke();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag(CompareTag))
        {
            CollisionExit.Invoke();
        }
    }
}
