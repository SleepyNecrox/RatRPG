using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CollectCheese : MonoBehaviour
{
    public CharacterManager characterManager;

    public QuestManager questManager;
    public GameObject MainQuestUI;
    public GameObject SideQuestUI;


    public TextMeshProUGUI MainQuestTXT;
    public TextMeshProUGUI SideQuestTXT;

    private bool hasCollected = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(characterManager.isCheeseCollectible && !hasCollected)
            {
                 if(Input.GetKey(KeyCode.Z))
                 {
                  Debug.Log("Collected");
                  this.gameObject.SetActive(false);
                  questManager.cheese += 1;

                  questManager.SideQuestCollect();
                  hasCollected = true; 
                 }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            
        }
    }

    private void LateUpdate()
    {
    hasCollected = false;
    }
}
