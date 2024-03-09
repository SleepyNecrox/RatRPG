using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManageGame : MonoBehaviour
{
    public static ManageGame Instance;

    public Vector3 playerPosition;
    public int cheeseCollected;
    private QuestManager questManager;

    public TextMeshProUGUI MainTXT;
    public TextMeshProUGUI SideTXT;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        questManager = FindObjectOfType<QuestManager>();
    }

    void Start()
    {
        LoadGame(); // Corrected
    }

    public void SaveGame()
    {
        PlayerPrefs.SetFloat("PlayerPosX", playerPosition.x);
        PlayerPrefs.SetFloat("PlayerPosY", playerPosition.y);
        PlayerPrefs.SetFloat("PlayerPosZ", playerPosition.z);
        PlayerPrefs.SetInt("CheeseCollected", cheeseCollected);
        PlayerPrefs.SetString("MainTXT", MainTXT.text);
        PlayerPrefs.SetString("SideTXT", SideTXT.text);

        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        playerPosition = new Vector3(
            PlayerPrefs.GetFloat("PlayerPosX", 0f),
            PlayerPrefs.GetFloat("PlayerPosY", 0f),
            PlayerPrefs.GetFloat("PlayerPosZ", 0f)
        );

        cheeseCollected = PlayerPrefs.GetInt("CheeseCollected", 0);
        MainTXT.text = PlayerPrefs.GetString("MainTXT", "");
        SideTXT.text = PlayerPrefs.GetString("SideTXT", "");
    }

    public void UpdatePlayerData(Vector3 newPosition)
    {
        playerPosition = newPosition;

        if (questManager != null)
        {
            cheeseCollected = questManager.cheese;

            if (questManager.MainQuestTXT != null)
            {
                MainTXT.text = questManager.MainQuestTXT.text;
            }
            if (questManager.SideQuestTXT != null)
            {
                SideTXT.text = questManager.SideQuestTXT.text;
            }
        }
    }
}
