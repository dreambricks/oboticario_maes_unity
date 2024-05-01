using UnityEngine;
using UnityEngine.UI;

public class CheckButtonBehavior : MonoBehaviour
{
    private Button toggle;
    private Image image;

    public bool isChecked;

    private void Start()
    {
     
    }
    private void OnEnable()
    {

        // Obtenha as referências aos componentes Toggle e Image
        toggle = GetComponent<Button>();
        image = GetComponent<Image>();

        isChecked = false;
        image.enabled = false;
    }

    public void OnCheck()
    {
        if (isChecked) 
        { 
            image.enabled = false;
            isChecked = false;
        }
        else
        {
            image.enabled = true;
            isChecked = true;
        }
    }
}
