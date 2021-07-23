using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private Rigidbody2D playerRB;
    [SerializeField] private float playerVerticalMoveSpeed = 5f;
    [SerializeField] private float playerHorizontalMoveSpeed = 3f;
    private float horizontalInput, verticalInput;
    private Animator playerAnim;
    private BoxCollider2D playerFeetCollider;
    private CapsuleCollider2D playerMainCollider;
    public Joystick joystick;
    private bool isGameActive = true;
    public ParticleSystem deathParticles;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
        playerMainCollider = GetComponent<CapsuleCollider2D>();
        deathParticles.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameActive)
        { 
        PlayerMovement();
        JumpAndClimb();
        Die();
        }
    }
    private void Die()
    {
        if(playerMainCollider.IsTouchingLayers(LayerMask.GetMask("enemies")))
        {
            playerAnim.SetTrigger("Die");
            isGameActive = false;
            deathParticles.gameObject.SetActive(true);
        }
    }
    private void JumpAndClimb()
    {
        verticalInput = joystick.Vertical;
        bool isTouchingLadder = playerMainCollider.IsTouchingLayers(LayerMask.GetMask("ladder"));
        if (verticalInput > 0.6f)
        {
            if(playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Foreground", "PlayerInteractable")) && !isTouchingLadder)
            { 
            playerAnim.SetBool("isJumping", true);
            playerRB.velocity = Vector2.up * playerVerticalMoveSpeed;
            }
            if(isTouchingLadder)
            {
                playerAnim.SetBool("isJumping", false);
                playerAnim.SetBool("isRunning", false);
                playerAnim.SetBool("isClimbing", true);
                playerRB.velocity = Vector2.up * playerVerticalMoveSpeed;
            }

        }
        else
        {
            playerAnim.SetBool("isJumping", false);
            playerAnim.SetBool("isClimbing", false);
        }
    }

    private void PlayerMovement()
    {
        float Joystickhorizontal = joystick.Horizontal;
        if (Mathf.Abs(Joystickhorizontal) > 0.5f)
        { 
        horizontalInput = Mathf.Sign(Joystickhorizontal);
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
        playerRB.velocity = new Vector2(playerHorizontalMoveSpeed * horizontalInput , playerRB.velocity.y );
    }
}
