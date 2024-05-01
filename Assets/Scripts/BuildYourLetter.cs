using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildYourLetter : MonoBehaviour
{

    public Button startButton;
    public CheckButtonBehavior checkButtonBehavior;
    public Text errorText;

    [SerializeField] private GameObject pickAPhrase;

    private void OnEnable()
    {
        checkButtonBehavior.isChecked = false;
        var check = checkButtonBehavior.GetComponent<Image>();
        check.enabled = false;
        errorText.enabled = false;
        startButton.onClick.AddListener(() => GoToPickAPhrase());
    }

    private void GoToPickAPhrase()
    {
        if (checkButtonBehavior.isChecked)
        {
            pickAPhrase.gameObject.SetActive(true);
            gameObject.SetActive(false);
        } else {
            errorText.enabled=true;
        }
        
    }


}
