using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private GameObject currentTeleporter;
    private float teleportCooldown = 1f; 
    private float lastTeleportTime;

    AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && CanTeleport())
        {
            if (currentTeleporter != null)
            {
                audioManager.PlaySFX(audioManager.Door);
                transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
                lastTeleportTime = Time.time;
            }
        }
    }

    private bool CanTeleport()
    {
        return Time.time - lastTeleportTime >= teleportCooldown;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
            }
        }
    }
}
