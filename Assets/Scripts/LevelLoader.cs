using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] public Animator transition;

    void Start()
    {
        LoadTransition();
    }

    private void LoadTransition()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2);
        transition.SetTrigger("End");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
}
