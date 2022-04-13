using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour,IGameResult
{
    public static SoundsManager singelton;
    public enum SoundsEffects { CreateSqares,OpenLetter,TrueOpenLetter,FailOpenLetter,Win,Loose }

    [SerializeField] AudioSource audioSource;
    [SerializeField] SoundsList_SO soundsList;

    private void Awake()
    {
        singelton = this;
    }
    private void Start()
    {
        InterfaceManager.singelton.AddIGameResultInArray(this);
    }
    public void PlaySoundsEffect(SoundsEffects _effectType)
    {
        audioSource.PlayOneShot(soundsList.soundsArray[(int)_effectType],0.7f);
    }

    public void IGameWin(int _pointCount = 0)
    {
        PlaySoundsEffect(SoundsEffects.Win);
    }

    public void IGameOver(int _pointCount = 0)
    {
        PlaySoundsEffect(SoundsEffects.Loose);
    }

    public void IGameNextLevel()
    {
        PlaySoundsEffect(SoundsEffects.TrueOpenLetter);
    }

    public void IGameRestart()
    {
        PlaySoundsEffect(SoundsEffects.TrueOpenLetter);
    }
}
