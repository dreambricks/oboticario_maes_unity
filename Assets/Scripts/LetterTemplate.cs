using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class LetterTemplate : MonoBehaviour
{
    [SerializeField] private Preview preview;
    [SerializeField] private PickAPhrase phrase;

    public Text deParaText;

    public GameObject objectToSave;
    public int width = 1080;
    public int height = 1920;

    public Sprite[] templates;

    private void OnEnable()
    {
        deParaText.text = "";

        PickATemplate();

        saveImage();
    }


    void saveImage()
    {
        deParaText.text = phrase.deText + "\n" + phrase.paraText;

        SaveAsImage();

        preview.gameObject.SetActive(true);
        gameObject.SetActive(false);
     
    }

    public void SaveAsImage()
    {
       
        RenderTexture renderTexture = new RenderTexture(width, height, 24);
        
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);

        Camera.main.targetTexture = renderTexture;
      
        Camera.main.Render();

        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        texture.Apply();

        byte[] bytes = texture.EncodeToPNG();

        string filePath = Application.streamingAssetsPath + "/savedImage.png";
        System.IO.File.WriteAllBytes(filePath, bytes);
        
        RenderTexture.active = null;
        Camera.main.targetTexture = null;
        Destroy(renderTexture);

        Debug.Log("Saved image as: " + filePath);
    }

    void PickATemplate()
    { 
        switch (phrase.phraseIndex)
        {
            case 0:
                objectToSave.GetComponent<Image>().sprite = templates[0];
                break;
            case 1:
                objectToSave.GetComponent<Image>().sprite = templates[1];
                break;
            case 2:
                objectToSave.GetComponent<Image>().sprite = templates[2];
                break;
            case 3:
                objectToSave.GetComponent<Image>().sprite = templates[3];
                break;
            case 4:
                objectToSave.GetComponent<Image>().sprite = templates[4];
                break;

        }
    }
}
