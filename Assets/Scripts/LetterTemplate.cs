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
    public int width = 2362;
    public int height = 3496;

    public Sprite[] templates;

    public Camera renderCamera;

    private void OnEnable()
    {
        deParaText.text = "";

        saveImage();
    }


    void saveImage()
    {
        deParaText.text = phrase.deText + "\n" + phrase.paraText;

        StartCoroutine(SaveAsImage());

    }

    IEnumerator SaveAsImage()
    {

        yield return new WaitForSeconds(1);

        PickATemplate();

        yield return new WaitForSeconds(1);

        RenderTexture renderTexture = new RenderTexture(width, height, 24);
        
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);

        renderCamera.targetTexture = renderTexture;

        renderCamera.Render();

        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        texture.Apply();

        byte[] bytes = texture.EncodeToPNG();

        string filePath = Application.streamingAssetsPath + "/savedImage.png";
        System.IO.File.WriteAllBytes(filePath, bytes);
        
        RenderTexture.active = null;
        Camera.main.targetTexture = null;
        //Destroy(renderTexture);
        yield return new WaitForSeconds(1);

        preview.gameObject.SetActive(true);
        gameObject.SetActive(false);

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
