using UnityEngine;

public class LerpTransform : MonoBehaviour {


    public Transform target;
    public float speed = 3.0f;

    void Update() {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime*speed);
        var lookRot = Quaternion.LookRotation(target.position);
        transform.rotation = Quaternion.Lerp(transform.rotation,lookRot,Time.deltaTime*speed);
    }

}