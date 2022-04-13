using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundsList", menuName = "ScriptableObjects/SoundsList", order = 1)]
public class SoundsList_SO : ScriptableObject
{
    public List<AudioClip> soundsArray;
}
