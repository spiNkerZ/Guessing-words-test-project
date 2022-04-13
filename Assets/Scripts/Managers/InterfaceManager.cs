using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterfaceManager : MonoBehaviour
{
    public static InterfaceManager singelton;

    List<IGuessedWord> iGuessedWordArray = new List<IGuessedWord>();
    List<IGameResult> iGameResultArray = new List<IGameResult>();

    private void Awake()
    {
        singelton = this;    
    }

    public void AddIGuessedWordInArray(IGuessedWord _iGussedWord)
    {
        iGuessedWordArray.Add(_iGussedWord);
    }

    public void AddIGameResultInArray(IGameResult _iGameResult)
    {
        iGameResultArray.Add(_iGameResult);
    }

    public void GussedWord(string _originalWord)
    {
        foreach (var item in iGuessedWordArray)
        {
            item.WordIsGuessed(_originalWord);
        }
    }

    public void GameWin(int _pointCount)
    {
        foreach (var item in iGameResultArray)
        {
            item.IGameWin(_pointCount);
        }
    }

    public void GameOver(int _pointCount)
    {
        foreach (var item in iGameResultArray)
        {
            item.IGameOver(_pointCount);
        }
    }

    public void ContinueGame()
    {
        foreach (var item in iGameResultArray)
        {
            item.IGameNextLevel();
        }
    }

    public void RestartGame()
    {
        foreach (var item in iGameResultArray)
        {
            item.IGameRestart();
        }
    }
}
