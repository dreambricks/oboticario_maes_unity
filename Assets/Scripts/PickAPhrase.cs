using BlackListOfWords;
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
    [SerializeField] GameObject preview;
    [SerializeField] private BuildYourLetter buildYourLetter;

    public string deText;
    public string paraText;
    public int phraseIndex;

    public string selectedButtonText;
    private Color deselectedColor;
    private Color selectedColor;
    private BlackList blacklist;

    public float totalTime;
    private float currentTime;

    private void OnEnable()
    {
        currentTime = totalTime;

        blacklist = new BlackList(Application.streamingAssetsPath + "/palavras.txt");
     
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

    private void Update()
    {
        Countdown();
    }

    void ButtonClicked(int buttonIndex)
    {
        
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

        okButton.onClick.RemoveAllListeners();
    }

    void GoToPreview()
    {
        if (selectedButtonText == null || selectedButtonText == "")
        {
            error.text = "Selecione uma frase.";
        }
        else if (deInputfield.text == "" || deInputfield.text.Length < 2)

        {
            error.text = "Escreva o seu nome";

        }
        else if (paraInputfield.text == "" || paraInputfield.text.Length < 2)

        {
            error.text = "Escreva o nome para quem está enviando a carta";

        }
        else if (blacklist.Valid(paraInputfield.text) == false || blacklist.Valid(deInputfield.text) == false)
        {
            error.text = "Nome inválido";
        }
        else
        {
            deText = deInputfield.text.ToUpper();
            paraText = paraInputfield.text.ToUpper();

            letterTemplate.SetActive(true);
            preview.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
  
    }

    public void Countdown()
    {
        currentTime -= Time.deltaTime;


        if (currentTime <= 0)
        {
            currentTime = 0;

            DataLog dataLog = new();
            dataLog.status = StatusEnum.FORMULARIO_INCOMPLETO.ToString();
            LogUtil.SendLogCSV(dataLog);

            buildYourLetter.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

}

