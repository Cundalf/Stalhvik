using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int _cantSouls;
    public int cantSouls
    {
        get
        {
            return _cantSouls;
        }
    }

    private int _souls;
    public int souls
    {
        get
        {
            return _souls;
        }
    }

    // Singleton
    private static GameManager sharedInstance = null;

    public static GameManager SharedInstance
    {
        get
        {
            return sharedInstance;
        }
    }

    private void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        sharedInstance = this;
        DontDestroyOnLoad(this);

        resetGame();
    }

    public void resetGame()
    {
        _souls = 0;
    }

    public void startGame()
    {
        resetGame();
        SceneManager.LoadScene("MainGame");
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void addSoul(int cant = 1)
    {
        _souls += cant;

        // TODO: Implementar interface de objetivos
        //GameUiManager uiManager = GameObject.Find("Canvas").GetComponent<GameUiManager>();
        /*
        if (uiManager == null)
        {
            Debug.LogError("No se encontro UIManager");
        }
        */

        if (_souls >= _cantSouls)
        {
            configEndGame();

            EndGameUI endGameCanvas = GameObject.Find("EndGameCanvas").GetComponent<EndGameUI>();
            endGameCanvas?.showWinFrame();
            AudioManager.SharedInstance.PlayNewTrack(2);
        }
    }

    public void endGame()
    {
        configEndGame();

        EndGameUI endGameCanvas = GameObject.Find("EndGameCanvas").GetComponent<EndGameUI>();
        endGameCanvas?.showLoseFrame();
        AudioManager.SharedInstance.PlayNewTrack(3);
    }

    private void configEndGame()
    {
        GameObject uiManager = GameObject.Find("Canvas");
        uiManager?.SetActive(false);

        GameObject mainCamera = GameObject.Find("MainCamera");
        mainCamera?.SetActive(false);

        GameObject endGameCamera = GameObject.Find("EndGameCamera");
        endGameCamera.GetComponent<Camera>().enabled = true;
        endGameCamera.GetComponent<AudioListener>().enabled = true;
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
