using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics; // Para iniciar e encontrar processos
using System.IO; // Para operações com caminhos de arquivo
using System.Runtime.InteropServices; // Para funções de API do Windows
using UnityEngine;

public class ShowVirtualKeyboard : MonoBehaviour
{
    private Process keyboardProcess;

    // Importa a função SendMessage da API do Windows
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

    // Constantes para mensagens de fechamento
    private const uint WM_SYSCOMMAND = 0x0112;
    private const int SC_CLOSE = 0xF060;

    public void ShowKeyboard()
    {
        string progFiles = @"C:\Program Files\Common Files\Microsoft Shared\ink";
        string keyboardPath = Path.Combine(progFiles, "TabTip.exe");
        keyboardProcess = Process.Start(keyboardPath);
    }


    public void HideKeyboardForced()
    {
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "taskkill",
            Arguments = "/F /IM TabTip.exe", // /F força o fechamento, /IM identifica pela imagem
            CreateNoWindow = true,
            UseShellExecute = false
        };
        Process.Start(psi);
    }

    // Adicione este método ao seu script
    public void HideKeyboard()
    {
        try
        {
            // Tenta encontrar a janela do teclado por classe para garantir o foco
            IntPtr hwnd = FindWindow("IPTip_Main_Window", null);
            if (hwnd != IntPtr.Zero)
            {
                // Envia a mensagem de fechamento diretamente para a classe do TabTip
                PostMessage(hwnd, WM_SYSCOMMAND, (IntPtr)SC_CLOSE, IntPtr.Zero);
            }
        }
        catch (System.Exception ex)
        {
            UnityEngine.Debug.LogError("Erro: " + ex.Message);
        }
    }




    // Imports necessários para essa abordagem
    [DllImport("user32.dll", SetLastError = true)]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
}
