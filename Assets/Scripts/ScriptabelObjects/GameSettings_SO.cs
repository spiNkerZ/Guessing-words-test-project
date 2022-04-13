using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSetting", menuName = "ScriptableObjects/GameSetting", order = 1)]
public class GameSettings_SO : ScriptableObject
{
    public int levelCounts;
    public int countAttempts;
    public int minWordLenght;
    public int maxWordLenght;
}
