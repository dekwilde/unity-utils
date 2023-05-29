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

    public UnityEvent CollisionEnter;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(CompareTag))
        {
            CollisionEnter.Invoke();
        }
    }
}
