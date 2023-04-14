using System;
using easyar;
using UnityEngine;

public class ImageTargetEvents : MonoBehaviour
{
    public ImageTargetController ImageTargetController;

    public event Action OnTargetFound;
    public event Action OnTargetLost;

    private void Awake()
    {
        if (ImageTargetController == null)
        {
            ImageTargetController = GetComponent<ImageTargetController>();
        }

        if (ImageTargetController != null)
        {
            ImageTargetController.TargetFound += () =>
            {
                Debug.Log("Target Found");
                OnTargetFound?.Invoke();
            };

            ImageTargetController.TargetLost += () =>
            {
                Debug.Log("Target Lost");
                OnTargetLost?.Invoke();
            };
        }
        else
        {
            Debug.LogError("ImageTargetController not found");
        }
    }
}
