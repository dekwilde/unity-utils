using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameLifeManager : MonoBehaviour
{
    [Header("Lista de Vidas (ícones ou objetos visuais)")]
    public List<GameObject> lives; // Lista de objetos que representam vidas

    [Header("Evento Disparado ao Perder Todas as Vidas")]
    public UnityEvent OnUserLoose; // Evento público disparado ao perder todas as vidas

    private int currentLives;

    void Start()
    {
        ResetLive(); // Inicia com todas as vidas ativas
    }

    // Reseta todas as vidas
    public void ResetLive()
    {
        currentLives = lives.Count;

        for (int i = 0; i < lives.Count; i++)
        {
            if (lives[i] != null)
                lives[i].SetActive(true);
        }
    }

    // Reduz uma vida e desativa o objeto correspondente
    public void UserHit()
    {
        if (currentLives <= 0)
            return;

        currentLives--;

        if (currentLives < lives.Count && lives[currentLives] != null)
        {
            lives[currentLives].SetActive(false);
        }

        if (currentLives <= 0)
        {
            UserLoose();
        }
    }

    // Dispara o evento de derrota
    public void UserLoose()
    {
        if (OnUserLoose != null)
        {
            OnUserLoose.Invoke();
        }
    }
}
