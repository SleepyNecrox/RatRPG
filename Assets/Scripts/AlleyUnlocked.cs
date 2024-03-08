using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlleyUnlocked : MonoBehaviour
{
 
    public CharacterManager characterManager;
    void Update()
    {
        if(characterManager.isAlleyUnlocked == true)
        {
            gameObject.SetActive(false);
        }
    }
}
