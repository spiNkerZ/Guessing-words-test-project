using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(SelectionWordHandler))]
public class SelectionWordInRunningLine : MonoBehaviour,IGuessedWord
{
    TextMeshProUGUI focusTMPWord;

    SelectionWordHandler wordHandler;

    bool focusTMPWordEncoded;

    event Action<bool> OnRunLine;
    event Action OnEncodeWord;

    List<string> oldWords = new List<string>();

    private void Awake()
    {
        wordHandler = GetComponent<SelectionWordHandler>();
    }

    public void Init(Action<bool> _onRunLine, Action _onEncodeWord)
    {
        OnRunLine += _onRunLine;
        OnEncodeWord += _onEncodeWord;
        InterfaceManager.singelton.AddIGuessedWordInArray(this);
    }
    
    void RunLineOn(bool _isRun) => OnRunLine.Invoke(_isRun);
    void EncodeWord() => OnEncodeWord.Invoke();

    public void SelectionProcess(TextMeshProUGUI[] _wordsTMPArray, float _rightBorder)
    {
        if (!focusTMPWord)
        {
            for (int i = 0; i < _wordsTMPArray.Length; i++)
            {
                if (CheckWordInFilters(_wordsTMPArray[i].text))
                {
                    focusTMPWord = _wordsTMPArray[i];

                    break;
                }
            }
        }

        if (focusTMPWord)
        {
            if (!focusTMPWordEncoded && focusTMPWord.transform.position.x > _rightBorder)
            {
                wordHandler.EncodeWord(focusTMPWord);
                focusTMPWordEncoded = true;
                OnEncodeWord();
            }

            if (focusTMPWord.transform.position.x < _rightBorder / 2)
            {
                SquareLettersManager.singleton.CreateSquares();

                RunLineOn(false);
            }
        }
    }

    bool CheckWordInFilters(string _word)
    {
        return CheckWordInOldWords(_word) && UniqueWordFilter.CheckWordInFiler(_word);
    }

    bool CheckWordInOldWords(string _word)
    {
       return !oldWords.Contains(_word);
    }

    public void WordIsGuessed(string _originalWord)
    {
        focusTMPWord.text = _originalWord;
        focusTMPWord.color = Color.white;
        focusTMPWord = null;
        focusTMPWordEncoded = false;
        oldWords.Add(_originalWord);
    }

    public void Restart(bool _clearOldWords)
    {
        if (_clearOldWords) oldWords = new List<string>();

        focusTMPWord = null;
        focusTMPWordEncoded = false;
    }
}
