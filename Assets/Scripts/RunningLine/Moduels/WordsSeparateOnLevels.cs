using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class WordsSeparateOnLevels : MonoBehaviour
{
    List<string[]> textLevelsArray = new List<string[]>();

    public void SeparateText(string[] _text, int _contsLevels)
    {
        if (_text.Length == 0) return;

        int remains = _text.Length;
        for (int i = 0; i < _contsLevels; i++)
        {
            int div = _text.Length / _contsLevels;
            remains -= div;
            if (_contsLevels - 1 == i) div += remains;
            textLevelsArray.Add(_text.Skip(i == 0 ? 0 : _text.Length - remains).Take(div).ToArray());
        }
    }

    public string[] GetTextByLevel(int _level)
    {
        return textLevelsArray[_level];
    }
}