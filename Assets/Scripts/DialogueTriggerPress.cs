using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private bool isInRange = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    void Update()
    {
        if(!PauseMenu.isPaused)
        {
            if (isInRange && Input.GetKeyDown(KeyCode.Z))
            {
            TriggerDialogue();
            }
        }
      
    }

    public void TriggerDialogue()
    {
        GetComponent<Collider2D>().enabled = false;

        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, this);
    }

    public void EnableCollider()
    {
        GetComponent<Collider2D>().enabled = true;
    }
}
