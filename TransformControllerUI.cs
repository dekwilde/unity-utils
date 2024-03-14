using UnityEngine;
using UnityEngine.UI;

public class TransformControllerUI : MonoBehaviour
{
    public GameObject targetObject; // O GameObject que será controlado
    public Slider posXSlider, posYSlider, posZSlider; // Sliders para posição
    public Slider rotXSlider, rotYSlider, rotZSlider; // Sliders para rotação
    public string uniqueID; // Identificador único para salvar/carregar as preferências

    private void Start()
    {
        // Verificar se existem dados salvos e aplicá-los
        LoadTransform();
    }

    private void Update()
    {
        // Atualizar a posição e a rotação do GameObject com base nos valores dos sliders
        UpdateTransform();
    }

    void UpdateTransform()
    {
        // Atualizar posição
        Vector3 newPos = new Vector3(posXSlider.value, posYSlider.value, posZSlider.value);
        targetObject.transform.position = newPos;

        // Atualizar rotação
        Quaternion newRot = Quaternion.Euler(rotXSlider.value, rotYSlider.value, rotZSlider.value);
        targetObject.transform.rotation = newRot;
    }

    public void SaveTransform()
    {
        // Salvar posição
        PlayerPrefs.SetFloat(uniqueID + "_posX", posXSlider.value);
        PlayerPrefs.SetFloat(uniqueID + "_posY", posYSlider.value);
        PlayerPrefs.SetFloat(uniqueID + "_posZ", posZSlider.value);

        // Salvar rotação
        PlayerPrefs.SetFloat(uniqueID + "_rotX", rotXSlider.value);
        PlayerPrefs.SetFloat(uniqueID + "_rotY", rotYSlider.value);
        PlayerPrefs.SetFloat(uniqueID + "_rotZ", rotZSlider.value);

        PlayerPrefs.Save(); // Não esqueça de salvar as alterações!
    }

    void LoadTransform()
    {
        // Carregar posição, se disponível
        if (PlayerPrefs.HasKey(uniqueID + "_posX") && PlayerPrefs.HasKey(uniqueID + "_posY") && PlayerPrefs.HasKey(uniqueID + "_posZ"))
        {
            float posX = PlayerPrefs.GetFloat(uniqueID + "_posX");
            float posY = PlayerPrefs.GetFloat(uniqueID + "_posY");
            float posZ = PlayerPrefs.GetFloat(uniqueID + "_posZ");
            targetObject.transform.position = new Vector3(posX, posY, posZ);

            // Atualizar os sliders de posição com os valores carregados
            posXSlider.value = posX;
            posYSlider.value = posY;
            posZSlider.value = posZ;
        }

        // Carregar rotação, se disponível
        if (PlayerPrefs.HasKey(uniqueID + "_rotX") && PlayerPrefs.HasKey(uniqueID + "_rotY") && PlayerPrefs.HasKey(uniqueID + "_rotZ"))
        {
            float rotX = PlayerPrefs.GetFloat(uniqueID + "_rotX");
            float rotY = PlayerPrefs.GetFloat(uniqueID + "_rotY");
            float rotZ = PlayerPrefs.GetFloat(uniqueID + "_rotZ");
            targetObject.transform.rotation = Quaternion.Euler(rotX, rotY, rotZ);

            // Atualizar os sliders de rotação com os valores carregados
            rotXSlider.value = rotX;
            rotYSlider.value = rotY;
            rotZSlider.value = rotZ;
        }
    }
}
