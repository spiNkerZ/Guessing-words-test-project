using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonsAcceptClicks : MonoBehaviour
{
    public void ContinueGame() => InterfaceManager.singelton.ContinueGame();
    public void RestartGame() => InterfaceManager.singelton.RestartGame();
}
