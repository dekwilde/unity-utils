using System.Collections.Generic;
using UnityEngine;

public class RandomActivate : MonoBehaviour
{
    [Header("Lista de Objetos")]
    public List<GameObject> objetos;

    // Desativa todos os objetos da lista
    public void ResetList()
    {
        foreach (GameObject obj in objetos)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }

    // Ativa um objeto aleatório da lista
    public void RandomList()
    {
        if (objetos == null || objetos.Count == 0)
        {
            Debug.LogWarning("Lista de objetos está vazia ou nula.");
            return;
        }

        ResetList(); // Garante que todos estejam desativados antes

        int index = Random.Range(0, objetos.Count);
        if (objetos[index] != null)
            objetos[index].SetActive(true);
    }
}
