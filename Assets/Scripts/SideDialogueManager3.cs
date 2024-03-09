using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SideDialogueManager3 : MonoBehaviour
{

    public CharacterManager characterManager;
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

    private SideQuestTrigger2 currentDialogueTrigger;
    public QuestManager questManager;

    public PlayerController playerController;

    AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    


    void Start()
    {
        sentences = new Queue<string>();
        playerMovement = FindObjectOfType<Movement>();
    }

    public void StartDialogue (Dialogue dialogue, SideQuestTrigger2 trigger)
    {
        Debug.Log("Dialgoue3");
        animator.SetBool("isOpen", true);
        if(isDialogue)
        {
            return;
        }

        isDialogue = true;
        playerMovement.ToggleMovement(false);
        currentDialogueTrigger = trigger;


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

         Debug.Log("Current Index: " + currentIndex);

        if (currentIndex >= 0 && currentIndex < currentDialogue.NPC_Names.Length)
        {
            nameTXT.text = currentDialogue.NPC_Names[currentIndex];
            nameTXTbg.text = currentDialogue.NPC_Names[currentIndex];
            PortraitIMG.sprite = currentDialogue.NPC_Portraits[currentIndex];
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
  
IEnumerator TypeSentence(string sentence)
{
    dialogueTXT.text = "";
    audioManager.PlaySFX(audioManager.Dialogue);
    foreach (char letter in sentence.ToCharArray())
    {
        dialogueTXT.text += letter;
        yield return new WaitForSeconds(0.03f);
    }
    audioManager.StopDialogueSFX();
}


 public void EndDialogue()
{
    ManageGame.Instance.SaveGame();

    //Debug.Log("Dialgoue3 END");
    animator.SetBool("isOpen", false);
    isDialogue = false;
    playerMovement.ToggleMovement(true);

    if (currentDialogueTrigger != null && currentDialogueTrigger.triggerType != SideQuestTrigger2.TriggerType.EnterCollider)
    {
        currentDialogueTrigger.EnableCollider();
        currentDialogueTrigger = null;
    }
    characterManager.isCheeseCollectible = true;
    questManager.SideQuestCollect();
    questManager.SideQuestShow();
    audioManager.StopDialogueSFX();
    StartCoroutine(LoadNextScene(4));
    
}


void Update()
{
    if(PauseMenu.isPaused == false)
    {
 if (Input.GetKeyDown(KeyCode.Z) && isDialogue)
    {
        audioManager.StopDialogueSFX();
        DisplayNextSentence();
    }
    }
   
}

 IEnumerator LoadNextScene(int sceneIndex)
{
    yield return new WaitForSeconds(1f);
    SceneManager.LoadScene(sceneIndex);
}



}