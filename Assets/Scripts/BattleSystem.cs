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

    AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        audioManager.PlaySFX(audioManager.Round1);
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
        audioManager.PlaySFX(audioManager.Punch);
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
    audioManager.PlaySFX(audioManager.Shoot);
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
    audioManager.PlaySFX(audioManager.Guard);
    int blockedDamage = Mathf.CeilToInt(enemyUnit.damage * 0f);
    int dealtDamage = Mathf.CeilToInt(playerUnit.damage * 0.3f);

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
    if (state == BattleState.WON || state == BattleState.LOST)
    {

        if (state == BattleState.WON)
        {
            SystemTXT.text = "VICTORY!";
            //ManageGame.Instance.LoadGame();
            StartCoroutine(LoadNextScene(3));
        }
        else if (state == BattleState.LOST)
        {
            SystemTXT.text = "DEFEAT!";
            //ManageGame.Instance.LoadGame();
            StartCoroutine(LoadNextScene(3));
        }
        
    }
}

    IEnumerator EnemyTurn() 
    {
        SystemTXT.text = enemyUnit.unitName + "'s Turn!";
        yield return new WaitForSeconds(2f);
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        audioManager.PlaySFX(audioManager.MetalPipe);
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

    IEnumerator LoadNextScene(int sceneIndex)
{
    yield return new WaitForSeconds(2f);
    SceneManager.LoadScene(sceneIndex);
}
}
