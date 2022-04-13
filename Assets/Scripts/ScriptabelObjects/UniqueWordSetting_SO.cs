using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UniqueWordSetting", menuName = "ScriptableObjects/UniqueWordSetting", order = 1)]
public class UniqueWordSetting_SO : ScriptableObject
{
    public List<char> vowelLettersArray = new List<char>();
    public int minNeedCountVowelLetters;
    public List<char> consonantLettersArray = new List<char>();
    public int minNeedCountConsonantLetters;
} 
