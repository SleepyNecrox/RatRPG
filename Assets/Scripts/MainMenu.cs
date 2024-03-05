using System.Collections;
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

    public Image fadeImage;
    public Animator fadeAnimator;

    private IEnumerator Start()
    {
        if (fadeImage != null)
        {
            fadeAnimator = fadeImage.GetComponent<Animator>();
            // Set the alpha value to 0 initially for transparency
            Color transparentColor = fadeImage.color;
            transparentColor.a = 0f;
            fadeImage.color = transparentColor;

            yield return StartCoroutine(FadeIn());
        }

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

    private void Update()
    {
        if (Input.GetKeyDown(_Key))
        {
            if (playButton != null)
            {
                StartCoroutine(FadeOutAndLoadScene(3));
            }
        }
    }

    public void PlayGame()
    {
        StartCoroutine(FadeOutAndLoadScene(3));
    }

    public void OptionsMenu()
    {
        StartCoroutine(FadeOutAndLoadScene(2));
    }

    public void StartMenu()
    {
        StartCoroutine(FadeOutAndLoadScene(1));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator FadeIn()
    {
        fadeAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(fadeAnimator.GetCurrentAnimatorStateInfo(0).length);
    }

    private IEnumerator FadeOutAndLoadScene(int sceneIndex)
    {
        fadeAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(fadeAnimator.GetCurrentAnimatorStateInfo(0).length);

        SceneManager.LoadScene(sceneIndex);
    }
}
