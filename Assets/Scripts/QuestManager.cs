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
        MainQuestHide();
        SideQuestHide();
    }

      void Update()
    {
        if(cheese == 5)
        {
            characterManager.isCheeseCollected = true;
            SideQuestCollected();
            cheese = 0;
            Debug.Log("Collected all 5");
        }
    }


    public void MainQuestHideout()
    {
        MainQuestTXT.text = "Go to the Parmesano Hideout and talk to Big Caesar";
    }

    public void MainQuestLocate()
    {
        MainQuestTXT.text = "Locate the Queso Hideout in the Alleyway";
    }


    public void SideQuestCollect()
    {
        SideQuestTXT.text = "Collect the Blue Cheese hidden around the Sewer City: " + cheese + "/5";
    }

     public void SideQuestCollected()
    {
        SideQuestTXT.text = "Return the Blue Cheese to Little Ched at the bar: 5/5";
    }

 public void MainQuestShow()
    {
        MainQuestUI.SetActive(true);
    }

    public void MainQuestHide()
    {
        MainQuestUI.SetActive(false);
    }
    public void SideQuestShow()
    {
        SideQuestUI.SetActive(true);
    }

    public void SideQuestHide()
    {
        SideQuestUI.SetActive(false);
    }


}