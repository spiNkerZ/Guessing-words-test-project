using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputLetter : MonoBehaviour
{
    [SerializeField] TMP_InputField inputFieldLetter;

    public void ClickOpenLetter()
    {
        if (inputFieldLetter.text != "")
        {
            SquareLettersManager.singleton.ClickOpenLetter(inputFieldLetter.text);
            inputFieldLetter.text = "";
        }
    }
}
