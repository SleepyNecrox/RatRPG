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
    public Animator animator;
    public Image NPC_PortraitIMG;
    public Image DialogueBox;

    public Sprite playerPortraitSprite;

    private Queue<string> sentences;
    private bool isDialogue = false;

    private Movement playerMovement;


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

        if (dialogue.NPC_Name == "Ronnie")
        {
            nameTXT.text = "Ronnie";
            nameTXTbg.text = "Ronnie";
            NPC_PortraitIMG.sprite = playerPortraitSprite;
            FlipUI(false);
        }
        else
        {
            nameTXT.text = dialogue.NPC_Name;
            nameTXTbg.text = dialogue.NPC_Name;
            NPC_PortraitIMG.sprite = dialogue.NPC_Portrait;
            FlipUI(true);
        }

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
        sentences.Enqueue(sentence);
        }
        playerMovement.ToggleMovement(false);
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueTXT.text = sentence;
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

private void FlipUI(bool isPlayer)
    {
        if (isPlayer)
        {
            DialogueBox.rectTransform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            DialogueBox.rectTransform.localScale = new Vector3(1, 1, 1);
        }
    }

void Update()
{
    if (Input.GetKeyDown(KeyCode.Z) && isDialogue)
    {
        DisplayNextSentence();
    }
}


}
