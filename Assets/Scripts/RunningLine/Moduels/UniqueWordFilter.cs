using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueWordFilter : MonoBehaviour
{
    [SerializeField] UniqueWordSetting_SO filerPreset;
    static UniqueWordSetting_SO filterPresetStatic;

    private void Awake()
    {
        filterPresetStatic = filerPreset;
    }

    public static bool CheckWordInFiler(string _word)
    {
        _word = _word.ToLower();

        int countsLetters = 0;
        int countsVowels = 0;
        int countsConsonants = 0;
        bool onlyLetters = false;

        foreach (char item in _word)
        {
            if(Char.IsLetter(item))
            {
                if(filterPresetStatic.vowelLettersArray.Contains(item)) countsVowels++;
                if(filterPresetStatic.consonantLettersArray.Contains(item)) countsConsonants++;
                countsLetters++;
            }
        }
        onlyLetters = countsLetters == _word.Length ? true : false;

        bool result = onlyLetters &&
                      countsLetters >= GameManager.singelton.gameSetting.minWordLenght &&
                      countsLetters <= GameManager.singelton.gameSetting.maxWordLenght &&
                      countsVowels >= filterPresetStatic.minNeedCountVowelLetters &&
                      countsConsonants >= filterPresetStatic.minNeedCountConsonantLetters;
       
        return result;
    }
}
