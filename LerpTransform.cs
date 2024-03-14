using UnityEngine;

public class LerpTransform : MonoBehaviour
{
    public Transform target;
    public float speed = 3.0f;

    public bool enableRotation = true;

    // Adicionando variáveis de offset para X, Y, Z
    public float offsetX = 0f;
    public float offsetY = 0f;
    public float offsetZ = 0f;

    void Update()
    {
        // Calcula a posição de destino com offset
        Vector3 targetPositionWithOffset = new Vector3(
            target.position.x + offsetX,
            target.position.y + offsetY,
            target.position.z + offsetZ
        );

        // Atualiza a posição com a posição de destino ajustada pelo offset
        transform.position = Vector3.Lerp(
            transform.position,
            targetPositionWithOffset,
            Time.deltaTime * speed
        );

        if (enableRotation)
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                target.rotation,
                Time.deltaTime * speed
            );
        }
    }
}
