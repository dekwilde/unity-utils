using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectClickEvent : MonoBehaviour
{
    // Prefab do objeto a ser instanciado
    public GameObject objectPrefab;

    // Cria um UnityEvent para disparar quando o objeto for clicado
    [SerializeField]
    private UnityEvent onClick;

    // Atualiza o método Update para verificar o clique do mouse
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Cria um raio a partir da câmera principal na direção do clique do mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Verifica se o raio atinge algum objeto com um Collider e se é o objeto que possui este script
            if (Physics.Raycast(ray, out hit) && hit.transform == this.transform)
            {
                // Obtém as coordenadas do ponto de impacto
                if (objectPrefab)
                {
                    Vector3 clickPoint = hit.point;
                    // Instancia o objeto no ponto de impacto
                    Instantiate(objectPrefab, clickPoint, Quaternion.identity);
                }

                // Dispara o evento OnClick
                onClick.Invoke();
            }
        }
    }
}
