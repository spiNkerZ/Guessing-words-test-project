using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionWordHandler : MonoBehaviour
{
    public void EncodeWord(TextMeshProUGUI _word)
    {
        _word.color = Color.green;
        SquareLettersManager.singleton.SetOriginalWord(_word.text);
        LittersUtilites.EncodeTMPtext(_word, '*');
    }

    public void UncodeWord(TextMeshProUGUI _word,string _originalWord)
    {
        _word.color = Color.white;
        _word.text = _originalWord;
    }

}
