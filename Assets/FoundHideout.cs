using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundHideout : MonoBehaviour
{
 
    public CharacterManager characterManager;
    void Update()
    {
        if(characterManager.isHideoutUnlocked == true)
        {
            gameObject.SetActive(false);
        }
    }
}
