using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;


    public AudioClip Monochrome;
    public AudioClip Menu;
    public AudioClip Background;
    public AudioClip Fight;
    public AudioClip Button;
    public AudioClip Door;
    public AudioClip Dialogue;  

    public AudioClip Round1;
    public AudioClip Punch;

    public AudioClip Shoot;
    public AudioClip Guard;

    public AudioClip MetalPipe;

    private void Start()
    {
        PlaySceneMusic();
    }

    private void PlaySceneMusic()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {

            case "00 Monochrome":
                musicSource.clip = Monochrome;
                break;

            case "01 Menu":
                musicSource.clip = Menu;
                break;

            case "02 Settings":
                musicSource.clip = Menu;
                break;

            case "03 Sewer":
                musicSource.clip = Background;
                break;
            case "04 Battle Area":
                musicSource.clip = Fight;
                break;

            default:
                musicSource.clip = Background;
                break;
        }

        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void StopDialogueSFX()
    {
        SFXSource.Stop();
    }
}
