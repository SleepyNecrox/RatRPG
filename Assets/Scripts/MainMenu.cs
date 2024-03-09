using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button optionsButton;
    public Button backButton;
    public Button quitButton;

    [SerializeField] Animator transition;

    AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

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


    public void PlayGame()
    {
        StartCoroutine(WaitOneSecond());
        audioManager.PlaySFX(audioManager.Button);
        StartCoroutine(TransitionLevel());
        SceneManager.LoadScene(3);
    }

    public void OptionsMenu()
    {
        StartCoroutine(WaitOneSecond());
        audioManager.PlaySFX(audioManager.Button);
        SceneManager.LoadScene(2);
    }

    public void StartMenu()
    {
        StartCoroutine(WaitOneSecond());
        audioManager.PlaySFX(audioManager.Button);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        StartCoroutine(WaitOneSecond());
        audioManager.PlaySFX(audioManager.Button);
        Application.Quit();
    }

    IEnumerator TransitionLevel()
    {
        transition.SetTrigger("End");
        yield return new WaitForSeconds(2);
    }

     IEnumerator WaitOneSecond()
    {
        yield return new WaitForSeconds(1);
    }


}