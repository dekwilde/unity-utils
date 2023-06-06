using UnityEngine;

public class LerpTransform : MonoBehaviour
{
    public Transform target;
    public float speed = 3.0f;

    void Update()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            target.position,
            Time.deltaTime * speed
        );

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            target.rotation,
            Time.deltaTime * speed
        );
    }
}
