using UnityEngine;
using UnityEngine.Events;

public class MyTargetEventHandler : MonoBehaviour
{
    public ImageTargetEvents ImageTargetEvents;

    public UnityEvent onFound;
    public UnityEvent onLost;
    
    private void Start()
    {
        ImageTargetEvents.OnTargetFound += TargetFound;
        ImageTargetEvents.OnTargetLost += TargetLost;
    }

    private void OnDestroy()
    {
        ImageTargetEvents.OnTargetFound -= TargetFound;
        ImageTargetEvents.OnTargetLost -= TargetLost;
    }

    private void TargetFound()
    {
        onFound.Invoke();
        // Faça algo quando o alvo for encontrado
        Debug.Log("Custom target found handler");
    }

    private void TargetLost()
    {
        onLost.Invoke();
        // Faça algo quando o alvo for perdido
        Debug.Log("Custom target lost handler");
    }
}
