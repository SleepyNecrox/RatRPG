using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public KeyCode _Key = KeyCode.Z;
    public Button playButton;
    public Button optionsButton;
    public Button backButton;
    public Button quitButton;

    void Start()
    {
        if (playButton != null)
        {
            playButton.onClick.AddListener(PlayGame);
        }

        else if (optionsButton != null)
        {
            optionsButton.onClick.AddListener(OptionsMenu);
        }

        else if (backButton != null)
        {
            backButton.onClick.AddListener(StartMenu);
        }

        else if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(_Key))
        {
            if (playButton != null)
            {
                playButton.onClick.Invoke();
            }
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(3);
    }

    public void OptionsMenu()
    {
        SceneManager.LoadScene(2);
    }

    public void StartMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
