using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private GameObject canDialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(canDialogue != null)
            {
                TriggerDialogue();
            }
        }
    }

private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            canDialogue = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(collision.gameObject == canDialogue)
            {
                canDialogue = null;
            }
            
        }
    }

}