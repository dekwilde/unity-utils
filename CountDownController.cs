using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CountDownController : MonoBehaviour
{

    public int countTotal;
    public GameObject countDown;
    public UnityEvent onStart;
    public UnityEvent onProgress;
    public UnityEvent onFinish;
    Text CountDownText;
    bool isCount;

    int count;
    
    void Start()
    {
        CountDownText = countDown.GetComponent<Text>();
        ResetCount();  
        
    }

    void ResetCount() {
        isCount = false;
        count = countTotal;
        CountDownText.text = "";

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
                CountDownText.text = count.ToString();
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
