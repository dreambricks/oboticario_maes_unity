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
    [SerializeField] GameObject letterTemplate;

    public string deText;
    public string paraText;
    public int phraseIndex;

    public string selectedButtonText;
    private Color deselectedColor;
    private Color selectedColor;

    private void OnEnable()
    {
        phraseIndex = 0;
        error.text = "";
        deText = "";
        paraText = "";

        deInputfield.text = "";
        paraInputfield.text = "";

        okButton.onClick.AddListener(() => GoToPreview());

        deselectedColor = new Color(83f / 255f, 91f / 255f, 76f / 255f, 1f);
        selectedColor = new Color(255f / 255f, 255f / 255f, 255f / 255f, 1f);

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Cria uma cópia local da variável 'i' para cada iteração
            buttons[i].onClick.AddListener(() => ButtonClicked(index));
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

    void ButtonClicked(int buttonIndex)
    {
        // Percorre todos os botões
        for (int i = 0; i < buttons.Length; i++)
        {
            
            Image image = buttons[i].GetComponent<Image>();
            Color color = image.color;
            color.a = (i == buttonIndex) ? 1f : 0f; 
            image.color = color;

            
            Text buttonText = buttons[i].GetComponentInChildren<Text>();
            buttonText.color = (i == buttonIndex) ? selectedColor : deselectedColor; 
        }

        
        selectedButtonText = buttons[buttonIndex].GetComponentInChildren<Text>().text;
        phraseIndex = buttonIndex;
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
            
            letterTemplate.SetActive(true);
            gameObject.SetActive(false);

        } 
        else if (deInputfield.text == "")

        {
            error.text = "Escreva o seu nome";

        }
        else if (paraInputfield.text == "")

        {
            error.text = "Escreva o nome para quem está enviando a carta";

        }
        else
        {
            error.text = "Selecione uma frase.";
        }
  
    }

}

