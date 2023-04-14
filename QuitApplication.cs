using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    // Função pública para fechar a aplicação
    public void QuitApp()
    {
        // Se estiver no editor do Unity, pare a reprodução
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Se estiver em uma plataforma de construção, feche a aplicação
            Application.Quit();
        #endif
    }
}
