using UnityEngine;
using System.Collections.Generic;

public class MouseInstantiate : MonoBehaviour
{
    public GameObject prefab;       // Prefab a ser instanciado
    public float fps = 10f;         // Intervalo de instância (objetos por segundo)

    private bool isInstantiating = false;
    private float timer = 0f;
    private List<GameObject> spawnedObjects = new List<GameObject>(); // Lista dos objetos instanciados

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isInstantiating = true;
            timer = 0f;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isInstantiating = false;
        }

        if (isInstantiating)
        {
            timer += Time.deltaTime;
            float interval = 1f / fps;

            if (timer >= interval)
            {
                timer -= interval;

                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 10f; // Distância da câmera (ajuste conforme necessário)

                Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                GameObject obj = Instantiate(prefab, worldPos, Quaternion.identity);
                spawnedObjects.Add(obj); // Adiciona à lista
            }
        }
    }

    // Apaga todos os objetos instanciados
    public void Reset()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        spawnedObjects.Clear(); // Limpa a lista
    }
}
