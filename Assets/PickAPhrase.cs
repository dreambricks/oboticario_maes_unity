using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class PickAPhrase : MonoBehaviour
{
    public Button[] buttons;
    public Button okButton;
    public Text error;
    public InputField deInputfield;
    public InputField paraInputfield;
    [SerializeField] GameObject preview;

    public string deText;
    public string paraText;

    public string selectedButtonText;
    private Color deselectedColor;
    private Color selectedColor;

    private void OnEnable()
    {
        error.text = "";
        deText = "";
        paraText = "";

        deInputfield.text = "";
        paraInputfield.text = "";

        okButton.onClick.AddListener(() => GoToPreview());

        deselectedColor = new Color(83f / 255f, 91f / 255f, 76f / 255f, 1f);
        selectedColor = new Color(255f / 255f, 255f / 255f, 255f / 255f, 1f);

        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => ButtonClicked(button));
        }

        foreach (Button button in buttons)
        {
            Image image = button.GetComponent<Image>();
            Color color = image.color;
            color.a = 0f;
            image.color = color;

            Text buttonText = button.GetComponentInChildren<Text>();
            buttonText.color = deselectedColor;
        }

        selectedButtonText = "";
    }

    void ButtonClicked(Button clickedButton)
    {
        
        foreach (Button button in buttons)
        {
            Image image = button.GetComponent<Image>();
            Color color = image.color;
            color.a = 0f;
            image.color = color;

            Text buttonText = button.GetComponentInChildren<Text>();
            buttonText.color = deselectedColor;
        }

        
        Image clickedButtonImage = clickedButton.GetComponent<Image>();
        Color clickedButtonColor = clickedButtonImage.color;
        clickedButtonColor.a = 1f;
        clickedButtonImage.color = clickedButtonColor;

        
        Text clickedButtonText = clickedButton.GetComponentInChildren<Text>();
        clickedButtonText.color = selectedColor;

        
        selectedButtonText = clickedButtonText.text;
    }

    private void OnDisable()
    {
        foreach (Button button in buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    void GoToPreview()
    {
        if (selectedButtonText != "")
        {

            deText = deInputfield.text;
            paraText = paraInputfield.text;
            
            preview.SetActive(true);
            gameObject.SetActive(false);

        } else
        {
            error.text = "Selecione uma frase.";
        }
  
    }

}

