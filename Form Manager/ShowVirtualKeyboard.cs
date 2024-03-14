using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // Para operações com caminhos de arquivo
using System.Diagnostics; // Para iniciar e encontrar processos

public class ShowVirtualKeyboard : MonoBehaviour
{
    public void ShowKeyboard()
    {
        string progFiles = @"C:\Program Files\Common Files\Microsoft Shared\ink";
        string keyboardPath = Path.Combine(progFiles, "TabTip.exe");
        Process.Start(keyboardPath);
    }

    public void HideKeyboard()
    {
        
        foreach (var proc in Process.GetProcessesByName("TabTip"))
        {
            proc.Kill(); // Fecha o processo do teclado virtual
        }
        
    }
}
