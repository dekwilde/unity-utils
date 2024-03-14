using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSelector : MonoBehaviour
{
    [Tooltip("Place the version as the following: [0] 'Português', [1] 'English', [2] 'Español'.")]
    [SerializeField] Image sourceImage;
    [SerializeField] Sprite[] version = new Sprite[3];


    private void Start()
    {
        Translate(LanguageManager.Instance.Language);
        LanguageManager.languageSelected += Translate;
    }

    private void Translate(int index)
    {
        Debug.Log("Changing image to selected language: " + index);
        if(version[index] != null)
            sourceImage.sprite = version[index];
    }
}
