using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosition : MonoBehaviour
{
    public Transform _transform;

    public void TransformPosition()
    {
        this.transform.position = _transform.position;
        this.transform.rotation = _transform.rotation;
    }
}
