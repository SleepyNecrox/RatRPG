using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDialogueTrigger1 : MonoBehaviour
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
                TriggerDialogue3();
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
            if (characterManager.isAlleyUnlocked && isInRange && Input.GetKeyDown(KeyCode.Z))
            {
                TriggerDialogue3();
            }

        }
    }

     public void TriggerDialogue3()
    {
        GetComponent<Collider2D>().enabled = false;
        FindObjectOfType<MainDialogueManager3>().StartDialogue(dialogue, this);
    }



    public void EnableCollider()
    {
        GetComponent<Collider2D>().enabled = true;
    }
}
