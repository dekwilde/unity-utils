using UnityEngine;
public class LookAtLerp : MonoBehaviour {

    public Transform target;
    public float speed = 1.0f;

    void Update() {
        var lookRot = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation,lookRot,Time.deltaTime*speed);
    }

}