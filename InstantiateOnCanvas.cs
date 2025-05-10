using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnCanvas : MonoBehaviour
{
   [Header("Prefab a ser instanciado")]
    public GameObject prefab;

    public void InstantiatePrefab()
    {
        if (prefab != null)
        {
            // Instancia como filho do mesmo pai (Canvas ou container)
            GameObject instance = Instantiate(prefab, transform.parent);
            
            // Alinha na mesma posição do botão
            RectTransform instanceRect = instance.GetComponent<RectTransform>();
            RectTransform buttonRect = GetComponent<RectTransform>();

            if (instanceRect != null && buttonRect != null)
            {
                instanceRect.anchoredPosition = buttonRect.anchoredPosition;
                instanceRect.localScale = buttonRect.localScale;
            }
        }
        else
        {
            Debug.LogWarning("Prefab não atribuído!");
        }
    }
}
