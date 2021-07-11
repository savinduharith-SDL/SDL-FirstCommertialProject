using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private Rigidbody2D playerRB;
    [SerializeField] private float playerMoveSpeed = 6f;
    private float horizontalInput, verticalInput;
    private Animator playerAnim;
    private BoxCollider2D playerFeetCollider;
    public Joystick joystick;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        Jump();
    }

    private void Jump()
    {
        verticalInput = joystick.Vertical;
        if(verticalInput > 0.6f && playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {
            playerAnim.SetBool("isJumping", true);
            playerRB.velocity = Vector2.up * playerMoveSpeed;
        }
        else
        {
            playerAnim.SetBool("isJumping", false);
        }
    }

    private void PlayerMovement()
    {
        if(Mathf.Abs(joystick.Horizontal) > 0.5f)
        { 
        horizontalInput = Mathf.Sign(joystick.Horizontal);
        }
        else
        {
            horizontalInput = 0;
        }
        if (Mathf.Abs(horizontalInput)> Mathf.Epsilon)
        {
            float signOfHorizontalInput = Mathf.Sign(horizontalInput);
            transform.localScale = new Vector2(signOfHorizontalInput, 1);
            playerAnim.SetBool("isRunning", true);
        }
        else
        {
            playerAnim.SetBool("isRunning", false);
        }
        playerRB.velocity = new Vector2(playerMoveSpeed * horizontalInput , playerRB.velocity.y );
    }
}
