using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public enum BattleState
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST,
}
public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;

    public GameObject enemyPrefab;

    public Transform PlayerLocation;

    public Transform EnemyLocation;

    Data playerUnit;

    Data enemyUnit;

    public TextMeshProUGUI EnemyNameTXT;

    public TextMeshProUGUI SystemTXT;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;



    public BattleState state;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject PlayerGO = Instantiate(playerPrefab, PlayerLocation);
        playerUnit = PlayerGO.GetComponent<Data>();

        GameObject EnemyGO = Instantiate(enemyPrefab, EnemyLocation);
        enemyUnit = EnemyGO.GetComponent<Data>();

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        SystemTXT.text = "Preparing The Fight!";

        yield return new WaitForSeconds(1f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        if(isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

        enemyHUD.SetHP(enemyUnit.currentHP);
        yield return new WaitForSeconds(2f);


    }


    IEnumerator PlayerShoot()
{
    for (int i = 0; i < 3; i++)
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.DoTdamage);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

        enemyHUD.SetHP(enemyUnit.currentHP);
        yield return new WaitForSeconds(2f);
    }
}

void PlayerGuard()
{

    int blockedDamage = Mathf.CeilToInt(enemyUnit.damage * 1.0f);
    int dealtDamage = Mathf.CeilToInt(playerUnit.damage * 0.25f);

    enemyUnit.TakeDamage(dealtDamage);
    enemyHUD.SetHP(enemyUnit.currentHP);

    playerUnit.TakeDamage(blockedDamage);
    playerHUD.SetHP(playerUnit.currentHP);

     if (enemyUnit.currentHP <= 0)
    {
        state = BattleState.WON;
        EndBattle();
    }
    else
    {
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

}

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            SystemTXT.text = "VICTORY!";
            StartCoroutine(WaitThreeSeconds());
            SceneManager.LoadScene(3);

        }
        else if (state == BattleState.LOST)
        {
            SystemTXT.text = "DEFEAT!";
            StartCoroutine(WaitThreeSeconds());
            SceneManager.LoadScene(3);
        }
    }

    IEnumerator EnemyTurn() 
    {
        SystemTXT.text = enemyUnit.unitName + "'s Turn!";
        yield return new WaitForSeconds(2f);
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        if(isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }

        else{
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void PlayerTurn()
    {
        SystemTXT.text = "Players Turn!";
    }

    public void OnAttack()
    {
        if(state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    public void OnShoot()
    {
        if(state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerShoot());
    }

    public void OnGuard()
    {
        if(state != BattleState.PLAYERTURN)
        {
            return;
        }

        PlayerGuard();
    }
   
    IEnumerator WaitThreeSeconds()
    {
        yield return new WaitForSeconds(3f);
    }
}
