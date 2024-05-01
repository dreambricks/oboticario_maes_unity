using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using System.Diagnostics;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class VirtualKeyboard : MonoBehaviour, ISelectHandler
{
    public KeyboardScript kbscript;
    public InputField input;

    public void OnSelect(BaseEventData eventData)
    {
        ShowKeyboard();
    }

    private void ShowKeyboard()
    {
        kbscript.TextField = input;
        kbscript.GetComponent<Animator>().Play("Up");
    }

    public void HideKeyboard()
    {
        kbscript.gameObject.SetActive(false);
    }


}