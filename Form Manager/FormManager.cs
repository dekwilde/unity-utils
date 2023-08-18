using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public class FormManager : MonoBehaviour
{
    private FormData formData;
    public InputField dado1Input; //nome? CRM? CPF?
    public InputField dado2Input; //email? telefone?
    public InputField dado3Input; //telefone? CPF?
    public UnityEvent onAdmin;
    public UnityEvent onSubmit;

    public string AdminPass;

    private void Start()
    {
        // Inicializar o objeto FormData
        formData = new FormData();
    }

    public void SubmitForm()
    {
        FillFormDataFromUI();
        if (dado1Input.text == AdminPass)
        {
            onAdmin.Invoke();
        }
        else
        {
            onSubmit.Invoke();
            SetRegister();
        }
    }

    public void SetRegister()
    {
        // Carregar a quantidade de registros existentes do PlayerPrefs
        int quantidadeRegistros = PlayerPrefs.GetInt("quantidadeDeRegistros", 0);
        // Incrementar a quantidade de registros
        quantidadeRegistros++;
        // Salvar o novo valor da quantidade de registros
        PlayerPrefs.SetInt("quantidadeDeRegistros", quantidadeRegistros);

        // Salvar os dados do registro atual usando o dado1 "user" + i
        string registroAtual = "user" + quantidadeRegistros;
        PlayerPrefs.SetString(registroAtual + "_dado1", formData.dado1);
        PlayerPrefs.SetString(registroAtual + "_dado2", formData.dado2);
        PlayerPrefs.SetString(registroAtual + "_dado3", formData.dado3);
    }

    public void GetRegistersAndExportCSV()
    {
        // Cabeçalho do arquivo CSV
        string csvContent = "dado1,dado2,dado3\n";

        // Carregar a quantidade de registros existentes do PlayerPrefs
        int quantidadeRegistros = PlayerPrefs.GetInt("quantidadeDeRegistros", 0);

        // Resgatar a lista de registros e adicioná-los ao conteúdo do CSV
        for (int i = 1; i <= quantidadeRegistros; i++)
        {
            string registroAtual = "user" + i;
            string dado1 = PlayerPrefs.GetString(registroAtual + "_dado1", "");
            string dado2 = PlayerPrefs.GetString(registroAtual + "_dado2", "");
            string dado3 = PlayerPrefs.GetString(registroAtual + "_dado3", "");

            // Adicionar os dados do registro ao conteúdo do CSV
            csvContent += $"{dado1},{dado2},{dado3}\n";
        }

        // Altere o caminho para escrever o arquivo CSV diretamente no persistentDataPath
        string filePath = Path.Combine(Application.temporaryCachePath, "registros.csv");
        File.WriteAllText(filePath, csvContent);

#if UNITY_ANDROID
		// Export the file
		NativeFilePicker.Permission permission = NativeFilePicker.ExportFile( filePath, ( success ) => Debug.Log( "File exported: " + success ) );

		Debug.Log( "Permission result: " + permission );

#elif UNITY_STANDALONE
        // No build Windows Desktop, abrir a janela do navegador com a URL para o arquivo
        Application.OpenURL(filePath);
#elif UNITY_EDITOR
        // No Unity Editor, abrir a janela do navegador com a URL para o arquivo
        Application.OpenURL(filePath);
#endif
    }



    public void FillFormDataFromUI()
    {
        if (dado1Input)
        {
            formData.dado1 = dado1Input.text;
        }
        else
        {
            formData.dado1 = "null";
        }
        if (dado2Input)
        {
            formData.dado2 = dado2Input.text;
        }
        else
        {
            formData.dado2 = "null";
        }
        if (dado3Input)
        {
            formData.dado3 = dado3Input.text;
        }
        else
        {
            formData.dado3 = "null";
        }
    }
}
