using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(SquareLettersActions))]
public class SquareLettersManager : MonoBehaviour,IGuessedWord,IGameResult
{
    public static SquareLettersManager singleton;

    [SerializeField] SquareLettersActions squareLettersActions;

    int countsLettersForOpen,countOpennedLetters;
  
    string originalWord = "";

    private void Awake()
    {
        SingleionInit();
    }

    private void Start()
    {
        Rebuild();

        InterfaceManager.singelton.AddIGuessedWordInArray(this);
        InterfaceManager.singelton.AddIGameResultInArray(this);
    }

    void SingleionInit()
    {
        singleton = this;
    }

    public void CreateSquares()
    {
        countsLettersForOpen = originalWord.Length;
        squareLettersActions.CreateSquares(originalWord);
    }
    
    public void ClickOpenLetter(string _letter)
    {
        squareLettersActions.ClickOpenLetter(_letter, LetterTruePlus, LetterNotInWord);
    }

    void LetterTruePlus()
    {
        countOpennedLetters++;

        if(countOpennedLetters == countsLettersForOpen)
        {
            SoundsManager.singelton.PlaySoundsEffect(SoundsManager.SoundsEffects.Win);
            InterfaceManager.singelton.GussedWord(originalWord);
        }
    }

    void LetterNotInWord()
    {
        SoundsManager.singelton.PlaySoundsEffect(SoundsManager.SoundsEffects.FailOpenLetter);
        GameManager.singelton.SubstactLife();
    }

    void Rebuild()
    {
        countOpennedLetters = 0;
        originalWord = "";
        squareLettersActions.Rebuild();
    }

    public void SetOriginalWord(string _word) => originalWord = _word;
    public void WordIsGuessed(string _originalWord) => Rebuild();
    public void IGameWin(int _pointCount = 0) => Rebuild();
    public void IGameOver(int _pointCount = 0) => Rebuild();
    public void IGameNextLevel() => Rebuild();
    public void IGameRestart() => Rebuild();
}
