using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;
    public static Action<int> languageSelected;
    public int Language { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        Language = 0;
    }

    public void SelectLanguage(int index)
    {
        Debug.Log("SelectLanguage");
        Language = index;
        languageSelected.Invoke(index);
    }
}
