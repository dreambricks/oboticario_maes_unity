using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;


public class QRCodePrint : MonoBehaviour
{
    public Button printButton;
    [SerializeField] private Image image;
    [SerializeField] private GameObject waitPrint;
    [SerializeField] private PickAPhrase phrase;
    [SerializeField] private BuildYourLetter buildYourLetter;

    public float totalTime;
    private float currentTime;


    private void OnEnable()
    {
        currentTime = totalTime;

        image.sprite = null;
        printButton.onClick.AddListener(() => PrintLetter());
    }

    private void Update()
    {
        TryConnectApi();
        Countdown();
    }

    void PrintLetter()
    {
        RunPrintByPowerShell();

        waitPrint.SetActive(true);
        gameObject.SetActive(false);
    }

    void RunPrintByPowerShell()
    {
        string powerShellScriptPath = Application.streamingAssetsPath + "/print_cp1500_v03.ps1";

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

    void TryConnectApi()
    {
        if (image.sprite == null)
        {
            GetNewQRCode();
        }
    }

    void GetNewQRCode()
    {
        string url = GameManager.GetAPIUrl();
        string fullUrl = url + "/qr/" + phrase.phraseIndex + "/" + phrase.deText + "/" + phrase.paraText;

        WebRequests.GetTexture(fullUrl,
            (string error) => { UnityEngine.Debug.Log("Error!\n" + error); },
            (Texture2D texture2D) =>
            {
                UnityEngine.Debug.Log("Success getting the QRCode!\n");
                Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(.5f, .5f), 16f);
                image.sprite = sprite;
            });
    }

    public void Countdown()
    {
        currentTime -= Time.deltaTime;


        if (currentTime <= 0)
        {
            currentTime = 0;

            DataLog dataLog = new();
            dataLog.status = StatusEnum.PAROU_EM_QRCODE.ToString();
            LogUtil.SendLogCSV(dataLog);

            buildYourLetter.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }


}
