using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public void SavePlayerPosition()
    {
        PlayerPrefs.SetFloat("PlayerPosX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", transform.position.z);
    }

    public void LoadPlayerPosition()
    {
        if (PlayerPrefs.HasKey("PlayerPosX") && PlayerPrefs.HasKey("PlayerPosY") && PlayerPrefs.HasKey("PlayerPosZ"))
        {
            float playerPosX = PlayerPrefs.GetFloat("PlayerPosX");
            float playerPosY = PlayerPrefs.GetFloat("PlayerPosY");
            float playerPosZ = PlayerPrefs.GetFloat("PlayerPosZ");

            transform.position = new Vector3(playerPosX, playerPosY, playerPosZ);
        }
        else
        {
            Debug.LogWarning("Player position data not found in PlayerPrefs.");
        }
    }
}
