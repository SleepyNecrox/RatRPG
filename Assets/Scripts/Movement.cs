using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] public Rigidbody2D rb;

    private bool isFacingRight = true;
    private bool canMove = true;
    Vector2 movement;

    void Update()
    {
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
        }
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
}
