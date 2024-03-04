using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string[] NPC_Names; 
    public Sprite[] NPC_Portraits;

    [TextArea(3, 10)]
    public string[] sentences;
}

