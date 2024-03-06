using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public KeyCode _Key = KeyCode.C;

    public GameObject pauseMenu;

    public Button playButton;
    public Button optionsButton;
    public Button backButton;
    public Button quitButton;

    public static bool isPaused;

    void Start()
    {
        pauseMenu.SetActive(false);

        if (playButton != null)
        {
            playButton.onClick.AddListener(ResumeGame);
        }

        else if (optionsButton != null)
        {
            optionsButton.onClick.AddListener(OptionsMenu);
        }

        else if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(_Key) && !DialogueManager.isDialogue)
        {
            if (isPaused)
            { 
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
       pauseMenu.SetActive(true);
       Time.timeScale = 0f;
       isPaused = true;
    }

    public void ResumeGame()
    {
       pauseMenu.SetActive(false);
       Time.timeScale = 1f;
       isPaused = false;
    }

    public void OptionsMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}