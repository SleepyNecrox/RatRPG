using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{

    public TextMeshProUGUI nameTXT;
    
    public Slider HPslider;

    
    public void SetHUD(Data data)
    {
        nameTXT.text = data.unitName;

        HPslider.maxValue = data.maxHP;

        HPslider.value = data.currentHP;

    }

    public void SetHP (int hp)
    {
        HPslider.value = hp;
    }
}
