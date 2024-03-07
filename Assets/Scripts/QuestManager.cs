using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{

    public CharacterManager characterManager;
    public GameObject MainQuestUI;
    public GameObject SideQuestUI;

    public TextMeshProUGUI MainQuestTXT;
    public TextMeshProUGUI SideQuestTXT;

    public int cheese = 0;

    void Start()
    {
        MainQuestUI.SetActive(false);
        SideQuestUI.SetActive(false);
    }

      void Update()
    {
        if(cheese == 5)
        {
            characterManager.isCheeseCollected = true;
            Debug.Log("Collected all 5");
        }
    }


    public void MainQuestLocate()
    {
        MainQuestTXT.text = "Go to the Parmesano Hideout and talk to Big Caesar";
    }

    public void MainQuestShow()
    {
        MainQuestUI.SetActive(true);
    }

    public void SideQuestCollect()
    {
        SideQuestTXT.text = "Collect and return the Blue Cheese hidden around the Sewer City: " + cheese + "/5";
    }

    public void SideQuestShow()
    {
        SideQuestUI.SetActive(true);
    }

}