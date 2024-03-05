using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLoadScene : MonoBehaviour
{
    private GameObject currentDoor;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && currentDoor != null)
        {
            SceneLoader sceneLoader = currentDoor.GetComponent<SceneLoader>();

            if (sceneLoader != null)
            {
                int SceneIndex = sceneLoader.SceneIndex;
                SceneManager.LoadScene(SceneIndex);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            currentDoor = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            if (collision.gameObject == currentDoor)
            {
                currentDoor = null;
            }
        }
    }
}
