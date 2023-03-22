using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform player;
    public Transform target;
    public float speed = 3.0f;
    float smoothTime = 0.3F;
    float velocity = 0.0f;
    public float height = 1.0f;
    public float distance = -1.5f;
    public float center = 1.0f;

    void Update() {
        //transform.position = player.transform.position + new Vector3(0, 0, 0.1f);
        transform.position = Vector3.Lerp(transform.position, player.transform.position + new Vector3(0, height, distance), Time.deltaTime*speed);
        //transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, velocity, smoothTime);

        var lookRot = Quaternion.LookRotation(target.position - transform.position + new Vector3(0, center, 0));
        transform.rotation = Quaternion.Lerp(transform.rotation,lookRot,Time.deltaTime*speed);
        //transform.LookAt(target, target.up);
    }

}