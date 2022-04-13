using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour,IGameResult
{
    public static GameManager singelton;
    public GameSettings_SO gameSetting;

    public int gameLevel;

    int lifeCount, pointCounts;

    private void Awake()
    {
        singelton = this;
    }

    private void Start()
    {
        Initialization();
    }

    void Initialization()
    {
        InterfaceManager.singelton.AddIGameResultInArray(this);
        lifeCount = gameSetting.countAttempts;
        UpdateUI();
    }

    public void AddPoints(int _value)
    {
        pointCounts += _value;
        UpdateUI();
    }

    public void SubstactLife()
    {
        lifeCount--;
        UpdateUI();

        if(lifeCount <= 0)
        {
            GAMEOVER();
        }
    }

    public void WIN()
    {
        InterfaceManager.singelton.GameWin(lifeCount);
    }
    public void GAMEOVER()
    {
        InterfaceManager.singelton.GameOver(pointCounts);
    }
 
    public void UpdateUI()
    {
        UIManager.singelton.SetLifes(lifeCount);
        UIManager.singelton.SetPoints(pointCounts);
    }

    public void IGameWin(int _pointCount)
    {
        pointCounts += lifeCount;
        UpdateUI();
    }

    public void IGameOver(int _pointCount)
    {
        Debug.Log("Game over");
    }

    public void IGameNextLevel()
    {
        gameLevel++;
    }

    public void IGameRestart()
    {
        gameLevel = 0;
        lifeCount = gameSetting.countAttempts;
        UpdateUI();
    }
}
