using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTransform : MonoBehaviour
{
    public Transform clone;
    public Transform target;

    public void TransformPosition()
    {
        target.position = clone.position;
    }

    public void TransformRotation()
    {
        target.rotation = clone.rotation;
    }
}
