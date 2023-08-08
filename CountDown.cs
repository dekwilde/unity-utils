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

    bool isCount;

    int count;
    
    void Start()
    {
        ResetCount();  
    }

    void ResetCount() {
        isCount = false;
        count = countTotal;
    }

    public void StartCount() {
        if(!isCount) {
            StartCoroutine(InitCount());
        }
        
    }

    public void StopCount() {
        ResetCount();
    }

    IEnumerator InitCount()
    {
        onStart.Invoke();
        isCount = true;
        while(isCount) {
            if(count>0) {
                onProgress.Invoke(count.ToString());
                count--;
                yield return new WaitForSeconds(1f);
            }
            else {
                onFinish.Invoke();
                ResetCount();
            }
        }
    }
}

[System.Serializable]
public class onProgressEvent : UnityEvent<string> { }
