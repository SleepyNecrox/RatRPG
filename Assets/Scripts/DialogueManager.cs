using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameTXT;
    public TextMeshProUGUI nameTXTbg;
    public TextMeshProUGUI dialogueTXT;
    public Image PortraitIMG;

    public Animator animator;
    public Sprite playerPortraitSprite;

    private Queue<string> sentences;
    private bool isDialogue = false;

    private Movement playerMovement;
    private Dialogue currentDialogue; 


    void Start()
    {
        sentences = new Queue<string>();
        playerMovement = FindObjectOfType<Movement>();
    }

    public void StartDialogue (Dialogue dialogue)
    {

        animator.SetBool("isOpen", true);
        if(isDialogue)
        {
            return;
        }

        isDialogue = true;
        playerMovement.ToggleMovement(false);

        sentences.Clear();


        if (dialogue.NPC_Names.Length == dialogue.NPC_Portraits.Length &&
        dialogue.NPC_Names.Length == dialogue.sentences.Length)
    {
        currentDialogue = dialogue;

        for (int i = 0; i < dialogue.sentences.Length; i++)
        {
            sentences.Enqueue(dialogue.sentences[i]);
        }

        DisplayNextSentence();
    }
    else
    {
        Debug.LogError("Dialogue lengths don't match!");
        EndDialogue();
        return;
    }

        playerMovement.ToggleMovement(false);
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueTXT.text = sentence;

        int currentIndex = currentDialogue.sentences.Length - sentences.Count - 1;

        if (currentIndex >= 0 && currentIndex < currentDialogue.NPC_Names.Length)
        {
            nameTXT.text = currentDialogue.NPC_Names[currentIndex];
            nameTXTbg.text = currentDialogue.NPC_Names[currentIndex];
            PortraitIMG.sprite = currentDialogue.NPC_Portraits[currentIndex];
        }
        else
        {
            Debug.LogError("Invalid index during DisplayNextSentence");
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
  
IEnumerator TypeSentence(string sentence)
{
    dialogueTXT.text = "";
    foreach (char letter in sentence.ToCharArray())
    {
        dialogueTXT.text += letter;
        yield return new WaitForSeconds(0.03f);
    }
}


  public void EndDialogue()
  {
    animator.SetBool("isOpen", false);
    isDialogue = false;
    playerMovement.ToggleMovement(true);
  }


void Update()
{
    if (Input.GetKeyDown(KeyCode.Z) && isDialogue)
    {
        DisplayNextSentence();
    }
}


}
