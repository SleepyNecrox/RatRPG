using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] public Rigidbody2D rb;

    private bool isFacingRight = true;
    private bool canMove = true;

    public float chanceForBattle = 0.1f;
    public LayerMask dangerZoneLayer; 

    private bool isInDangerZone = false;

    Vector2 movement;

    void Update()
    {
        //Debug.Log("isInDangerZone: " + isInDangerZone);
        if (!DialogueManager.isDialogue && !PauseMenu.isPaused)
        {
            if (canMove)
            {
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");


                //if(movement.x != 0) movement.y = 0;
                
                movement.Normalize();

                Flip();
            }
            if (isInDangerZone && Random.value < chanceForBattle)
            {
            StartRandomBattle();
            }
            
        }
        ManageGame.Instance.UpdatePlayerData(transform.position);
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DangerZone"))
        {
            isInDangerZone = true;
            Debug.Log("Entered DangerZone");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("DangerZone"))
        {
            isInDangerZone = false;
            Debug.Log("Exited DangerZone");
        }
    }

    private void Flip()
    {
        if (isFacingRight && movement.x < 0f || !isFacingRight && movement.x > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void ToggleMovement(bool enableMovement)
    {
        canMove = enableMovement;

        if (!enableMovement)
        {
            rb.velocity = Vector2.zero;
        }
    }

    void StartRandomBattle()
    {
        SceneManager.LoadScene(4);
    }
}
