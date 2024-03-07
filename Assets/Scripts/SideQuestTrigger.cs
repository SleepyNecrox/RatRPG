using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideQuestTrigger : MonoBehaviour
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
            if (!characterManager.isCheeseCollectible && isInRange && Input.GetKeyDown(KeyCode.Z))
            {
                TriggerDialogue();
            }
        }
    }

    public void TriggerDialogue()
    {
        GetComponent<Collider2D>().enabled = false;
        FindObjectOfType<SideDialogueManager>().StartDialogue(dialogue, this);
    }

    public void EnableCollider()
    {
        GetComponent<Collider2D>().enabled = true;
    }
}
