using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject buildYourLetter;
    [SerializeField] private GameObject pickAPhrase;
    [SerializeField] private GameObject preview;
    [SerializeField] private GameObject qRCodePrint;
    [SerializeField] private GameObject letterTemplate;

    private void Awake()
    {
        buildYourLetter.gameObject.SetActive(true);
        pickAPhrase.gameObject.SetActive(false);
        preview.gameObject.SetActive(false);
        letterTemplate.gameObject.SetActive(false);
        qRCodePrint.gameObject.SetActive(false);
    }
} 
