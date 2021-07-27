using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameUI : MonoBehaviour
{
    public GameObject WinFrameGO;
    public GameObject LoseFrameGO;

    public void showWinFrame()
    {
        WinFrameGO.SetActive(true);
    }

    public void showLoseFrame()
    {
        LoseFrameGO.SetActive(true);
    }

    public void goToMainMenu()
    {
        GameManager.SharedInstance.goToMainMenu();
    }
}
