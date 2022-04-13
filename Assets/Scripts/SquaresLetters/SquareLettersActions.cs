using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SquareLettersActions : MonoBehaviour
{
    [SerializeField] Transform[] sqaureLettersArray;
    [SerializeField] SquareLetter[] lettersInSquareArray;

    public string originalWord = "";

    int countsLettersForOpen;

    List<string> lettersRepeatArray = new List<string>();

    public void Init(ref string _originalWord, ref int _countsLettersForOpen)
    {
        originalWord = _originalWord;
        countsLettersForOpen = _countsLettersForOpen;
    }

    public void CreateSquares(string _word)
    {
        originalWord = _word; countsLettersForOpen = _word.Length;

        CreateSqareLettersArray();
        CloseAllSquares();
        SetLattersInSqares();

        SoundsManager.singelton.PlaySoundsEffect(SoundsManager.SoundsEffects.CreateSqares);
    }

    public void CloseAllSquares()
    {
        for (int i = 0; i < sqaureLettersArray.Length; i++)
        {
            lettersInSquareArray[i].CloseLetter();
        }
    }

    public void DeactiveAllSqares()
    {
        foreach (var item in lettersInSquareArray)
        {
            item.DeactiveLetter();
        }
    }

    public void ClickOpenLetter(string _letter, Action _letterTruePlus, Action _letterNotInWord)
    {
        if (!sqaureLettersArray[0].gameObject.activeSelf) return;

        if (CheckLetterOnRepeat(_letter) && CheckLetterInWord(_letter, out var _lettersIDArray))
        {
            foreach (var item in _lettersIDArray)
            {
                OpenCurrectLetterInWord(item);
                _letterTruePlus.Invoke();
            }

            AddRepaitLetter(_letter);
        }
        else
        {
            _letterNotInWord.Invoke();
        }
    }
    public void OpenCurrectLetterInWord(int _letterID)
    {
        lettersInSquareArray[_letterID].OpenLetter();
        SoundsManager.singelton.PlaySoundsEffect(SoundsManager.SoundsEffects.TrueOpenLetter);
    }

    public void CreateSqareLettersArray()
    {
        for (int i = 0; i < countsLettersForOpen; i++)
        {
            lettersInSquareArray[i].ActiveLetter();
        }
    }

    public void SetLattersInSqares()
    {
        for (int i = 0; i < originalWord.Length; i++)
        {
            lettersInSquareArray[i].SetLetter(originalWord[i].ToString());
        }
    }

    public bool CheckLetterOnRepeat(string _letter)
    {
        return !lettersRepeatArray.Contains(_letter);
    }

    public bool CheckLetterInWord(string _letter, out List<int> _lettersIDArray)
    {
        string checkWord = originalWord.ToLower();

        _letter = _letter.ToLower();
        _lettersIDArray = new List<int>();

        for (int i = 0; i < checkWord.Length; i++)
        {
            if (checkWord[i] == _letter[0])
            {
                _lettersIDArray.Add(i);
            }
        }

        return _lettersIDArray.Count > 0;
    }

    public void Rebuild()
    {
        DeactiveAllSqares();
        originalWord = "";
        countsLettersForOpen = 0;
        lettersRepeatArray = new List<string>();
    }

    public void AddRepaitLetter(string _letter) => lettersRepeatArray.Add(_letter);
}
