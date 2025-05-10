using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public int countTotal;
    public GameObject countDown;
    public UnityEvent onStart;
    public UnityEvent onProgress;
    public UnityEvent onFinish;

    private bool isCount;
    private int count;
    private Coroutine countCoroutine;

    void Start()
    {
        ResetCount();
    }

    void ResetCount()
    {
        isCount = false;
        count = countTotal;

        if (countDown)
        {
            countDown.GetComponent<Text>().text = "";
        }
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
                if (countDown)
                {
                    countDown.GetComponent<Text>().text = count.ToString();
                }
                
                onProgress.Invoke();
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
