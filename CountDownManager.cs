using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CountDownManager : MonoBehaviour
{

    public int countTotal;
    public GameObject countDown;
    public UnityEvent onStart;
    public UnityEvent onProgress;
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
        if(countDown) {
            countDown.GetComponent<Text>().text = "";
        }
        

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
                if(countDown) {
                    countDown.GetComponent<Text>().text = count.ToString();
                }
                onProgress.Invoke();
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
