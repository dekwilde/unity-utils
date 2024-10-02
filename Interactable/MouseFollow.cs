// =================================
// Namespaces.
// =================================

using UnityEngine;

// =================================
// Classes.
// =================================

public class MouseFollow : MonoBehaviour
{
    // =================================
    // Nested classes and structures.
    // =================================

    // ...

    // =================================
    // Variables.
    // =================================

    // ...

    public float speed = 8.0f;
    public float distanceFromCamera = 5.0f;
    public bool ignoreTimeScale;

    // Nova variável pública para anexar a câmera
    public Camera targetCamera;

    // =================================
    // Functions.
    // =================================

    // ...

    void Awake() { }

    // ...

    void Start() 
    {
        // Se a câmera não for definida no Inspector, utiliza Camera.main como fallback
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

    // ...

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = distanceFromCamera;

        // Usa a câmera definida na variável pública
        Vector3 mouseScreenToWorld = targetCamera.ScreenToWorldPoint(mousePosition);

        float deltaTime = !ignoreTimeScale ? Time.deltaTime : Time.unscaledDeltaTime;
        Vector3 position = Vector3.Lerp(
            transform.position,
            mouseScreenToWorld,
            1.0f - Mathf.Exp(-speed * deltaTime)
        );

        transform.position = position;
    }

    // ...

    void LateUpdate() { }

    // =================================
    // End functions.
    // =================================
}
