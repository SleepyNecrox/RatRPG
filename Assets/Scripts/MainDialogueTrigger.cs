using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDialogueTrigger : MonoBehaviour
{
    public enum TriggerType
    {
        PressZ,
        EnterCollider
    }

    public TriggerType triggerType = TriggerType.PressZ;
    public Dialogue dialogue;
    public CharacterManager characterManager;


    private bool isInRange = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            if (triggerType == TriggerType.EnterCollider)
            {
                TriggerDialogue();
            }
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
        if (!PauseMenu.isPaused && triggerType == TriggerType.PressZ)
        {
            if (!characterManager.isAlleyUnlocked && isInRange && Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("1");
                TriggerDialogue();
            }
            if (characterManager.isAlleyUnlocked && characterManager.isHideoutFound && isInRange && Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("2");
                TriggerDialogue2();
            }

        }
    }

    public void TriggerDialogue()
    {
        GetComponent<Collider2D>().enabled = false;
        FindObjectOfType<MainDialogueManager>().StartDialogue(dialogue, this);
        GetComponent<MainDialogueTrigger>().enabled = false;
    }

    public void TriggerDialogue2()
    {
        GetComponent<Collider2D>().enabled = false;
        FindObjectOfType<MainDialogueManager2>().StartDialogue(dialogue, this);
        GetComponent<MainDialogueTrigger>().enabled = false;
    }

    public void EnableCollider()
    {
        GetComponent<Collider2D>().enabled = true;
    }
}
