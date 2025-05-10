using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateController : MonoBehaviour
{
    public GameObject obj;
    // Update is called once per frame
    public void DoInstantiate()
    {
        Instantiate(obj, transform.position, transform.rotation);
    }
}
