using System.Diagnostics;
using System.Drawing.Printing;
using UnityEngine;
using UnityEngine.UI;


public class QRCodePrint : MonoBehaviour
{
    public Button printButton;
    public string imagePath;
    [SerializeField] private GameObject buildYourLetter;


    private void OnEnable()
    {
        imagePath = Application.streamingAssetsPath + "/savedImage.png";
        printButton.onClick.AddListener(() => PrintLetter());
    }

    void PrintLetter()
    {
        RunPrintByPowerShell();

        buildYourLetter.SetActive(true);
        gameObject.SetActive(false);
    }

    void RunPrintByPowerShell()
    {
        string powerShellScriptPath = Application.streamingAssetsPath + "/runprint.ps1";

        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments = $"-ExecutionPolicy Bypass -File \"{powerShellScriptPath}\"",
            UseShellExecute = false,
            CreateNoWindow = true
        };

        // Inicia o processo
        Process process = Process.Start(psi);
        process.WaitForExit();
    }



}
