using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour, IGameResult
{
    public static UIManager singelton;

    [SerializeField] TextMeshProUGUI countPoint, countLifes,countPointsInPanel;
    [SerializeField] Transform gameResult,winPanel,loosePanel;

    private void Awake()
    {
        singelton = this;
    }

    private void Start()
    {
        InterfaceManager.singelton.AddIGameResultInArray(this);
    }

    public void ActiveWinPanelAndSetPoints(bool _active,int _addPoints = 0)
    {
        gameResult.gameObject.SetActive(_active);
        winPanel.gameObject.SetActive(_active);

        countPointsInPanel.text = _addPoints.ToString();
    }
    public void ActiveLoosePanelAndSetPoints(bool _active, int _addPoints = 0)
    {
        gameResult.gameObject.SetActive(_active);
        loosePanel.gameObject.SetActive(_active);

        countPointsInPanel.text = _addPoints.ToString();
    }

    public void SetPoints(int _newValue) => countPoint.text = _newValue.ToString();
    public void SetLifes(int _newValue) => countLifes.text = _newValue.ToString();
    public void IGameOver(int _pointCount) => ActiveLoosePanelAndSetPoints(true, _pointCount);
    public void IGameWin(int _pointCount) => ActiveWinPanelAndSetPoints(true, _pointCount);
    public void IGameNextLevel() => ActiveWinPanelAndSetPoints(false);
    public void IGameRestart() => ActiveLoosePanelAndSetPoints(false);
}
