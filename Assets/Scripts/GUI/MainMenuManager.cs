using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject optionsFrame;
    public GameObject storyFrame;
    public GameObject tutorialFrame;

    public void showStory()
    {
        optionsFrame.SetActive(false);
        storyFrame.SetActive(true);
    }

    public void showTutorial()
    {
        optionsFrame.SetActive(false);
        tutorialFrame.SetActive(true);
    }

    public void closeTutorial()
    {
        optionsFrame.SetActive(true);
        tutorialFrame.SetActive(false);
    }

    public void startGame()
    {
        GameManager.SharedInstance.startGame();
    }

    public void closeGame()
    {
        GameManager.SharedInstance.exitGame();
    }
}
