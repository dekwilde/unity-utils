using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerPrefabSliderController : MonoBehaviour
{

    public Slider slider;
    public string uniqueID; // Identificador único para salvar/carregar as preferências

    public UnityEvent<float> onLoadData;

    private void Start()
    {
        // Verificar se existem dados salvos e aplicá-los
        LoadData();
    }


    public void SaveData()
    {
        PlayerPrefs.SetFloat(uniqueID, slider.value);
        PlayerPrefs.Save(); // Não esqueça de salvar as alterações!
    }

    void LoadData()
    {
        // Carregar posição, se disponível
        if (PlayerPrefs.HasKey(uniqueID))
        {
            float value = PlayerPrefs.GetFloat(uniqueID);
            // Atualizar os sliders de posição com os valores carregados
            slider.value = value;
            onLoadData.Invoke(value);

        }
    }
}
