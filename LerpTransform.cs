using UnityEngine;

public class LerpTransform : MonoBehaviour
{
    public Transform target;
    public float speed = 3.0f;
    public bool enableRotation = true;

    // Variáveis de offset para X, Y, Z
    public float offsetX = 0f;
    public float offsetY = 0f;
    public float offsetZ = 0f;

    // Variáveis para o efeito de elasticidade
    public bool enableBounce = false;
    public float bounceIntensity = 1.5f; // Controle da intensidade do bounce (elasticidade)

    void Update()
    {
        // Calcula a posição de destino com offset
        Vector3 targetPositionWithOffset = new Vector3(
            target.position.x + offsetX,
            target.position.y + offsetY,
            target.position.z + offsetZ
        );

        // Aplicando efeito de bounce se ativado
        if (enableBounce)
        {
            transform.position = BounceLerp(transform.position, targetPositionWithOffset, Time.deltaTime * speed, bounceIntensity);
        }
        else
        {
            // Transição suave sem bounce
            transform.position = Vector3.Lerp(
                transform.position,
                targetPositionWithOffset,
                Time.deltaTime * speed
            );
        }

        if (enableRotation)
        {
            if (enableBounce)
            {
                transform.rotation = BounceLerp(transform.rotation, target.rotation, Time.deltaTime * speed, bounceIntensity);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(
                    transform.rotation,
                    target.rotation,
                    Time.deltaTime * speed
                );
            }
        }
    }

    // Função para interpolar com bounce (elasticidade)
    Vector3 BounceLerp(Vector3 start, Vector3 end, float t, float intensity)
    {
        t = Mathf.Clamp01(t); // Garantindo que t esteja entre 0 e 1
        t = Mathf.PingPong(t * intensity, 1); // Adiciona o efeito de ping pong (bounce)

        return Vector3.Lerp(start, end, t);
    }

    // Overload para rotacionar com bounce (elasticidade)
    Quaternion BounceLerp(Quaternion start, Quaternion end, float t, float intensity)
    {
        t = Mathf.Clamp01(t);
        t = Mathf.PingPong(t * intensity, 1);

        return Quaternion.Lerp(start, end, t);
    }
}
