using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button twoPlayerButton;
    public Button settingsButton;
    public Button quitButton;
    public Button aboutButton;
    public Button exitButton;

    void Start()
    {
        if (twoPlayerButton != null)
        {
            twoPlayerButton.onClick.AddListener(TwoPlayer);
        }

        if (settingsButton != null)
        {
            settingsButton.onClick.AddListener(OpenSettings);
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }

        if (aboutButton != null)
        {
            aboutButton.onClick.AddListener(About);
        }

        if (exitButton != null)
        {
            exitButton.onClick.AddListener(Exit);
        }
    }

    public void TwoPlayer()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void About()
    {
        SceneManager.LoadScene("About");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Exit()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
