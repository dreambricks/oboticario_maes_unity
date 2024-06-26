using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Preview : MonoBehaviour
{
    private string imagePath; 
    public GameObject panel;
    public Button continueButton;
    public Text carregando;

    [SerializeField] private GameObject qrcode;
    [SerializeField] private BuildYourLetter buildYourLetter;


    public float totalTime;
    private float currentTime;

    private void OnEnable()
    {
        currentTime = totalTime;

        panel.GetComponent<Image>().sprite = null;

        StartCoroutine(LoadPreview());
        carregando.enabled = true;
    }

    private void Update()
    {
        Countdown();
    }

    IEnumerator LoadPreview()
    {

        yield return new WaitForSeconds(4);

        continueButton.onClick.AddListener(() => GoToQRCode());

        imagePath = Application.streamingAssetsPath + "/savedImage.png";


        if (!string.IsNullOrEmpty(imagePath))
        {

            Texture2D texture = LoadTextureFromFile(imagePath);


            if (texture != null)
            {

                Image panelImage = panel.GetComponent<Image>();


                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                panelImage.sprite = sprite;

                carregando.enabled = false;
            }
            else
            {
                Debug.LogError("Falha ao carregar a imagem: " + imagePath);
            }
        }
        else
        {
            Debug.LogError("Caminho da imagem vazio.");
        }
    }

    
    private Texture2D LoadTextureFromFile(string filePath)
    {
        
        if (System.IO.File.Exists(filePath))
        {
            
            byte[] fileData = System.IO.File.ReadAllBytes(filePath);

            
            Texture2D texture = new Texture2D(2, 2);
            
            texture.LoadImage(fileData);

            return texture;
        }
        else
        {
            Debug.LogError("Arquivo n�o encontrado: " + filePath);
            return null;
        }
    }

    void GoToQRCode()
    {
        qrcode.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Countdown()
    {
        currentTime -= Time.deltaTime;


        if (currentTime <= 0)
        {
            currentTime = 0;

            DataLog dataLog = new();
            dataLog.status = StatusEnum.PAROU_EM_PREVIEW.ToString();
            LogUtil.SendLogCSV(dataLog);

            buildYourLetter.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
