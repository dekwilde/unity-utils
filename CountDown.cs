using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public int countTotal;
    public UnityEvent onStart;
    public onProgressEvent onProgress;
    public UnityEvent onFinish;

    private bool isCount;
    private int count;
    private Coroutine countCoroutine;

    void Awake()
    {
        ResetCount();
    }

    void ResetCount()
    {
        isCount = false;
        count = countTotal;
    }

    public void StartCount()
    {
        if (countCoroutine != null)
        {
            StopCoroutine(countCoroutine);
        }

        countCoroutine = StartCoroutine(InitCount());
    }

    public void StopCount()
    {
        if (countCoroutine != null)
        {
            StopCoroutine(countCoroutine);
            countCoroutine = null;
        }

        ResetCount();
    }

    IEnumerator InitCount()
    {
        onStart.Invoke();
        isCount = true;

        while (isCount)
        {
            if (count > 0)
            {
                onProgress.Invoke(count.ToString());
                count--;
                yield return new WaitForSeconds(1f);
            }
            else
            {
                onFinish.Invoke();
                ResetCount();
                countCoroutine = null;
                yield break;
            }
        }
    }
}

[System.Serializable]
public class onProgressEvent : UnityEvent<string> { }