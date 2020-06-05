﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    [Tooltip("Multiplier for player run speed")]
    [SerializeField] float runSpeed = 5f;
    [Tooltip("Multiplier for player ladder climb speed")]
    [SerializeField] float climbSpeed = 5f;
    [Tooltip("Amount to add to player's Y velocity when jumping")]
    [SerializeField] float jumpSpeed = 5f;

    Rigidbody2D myRigidBody = null;
    Animator myAnimator = null;
    Collider2D myCollider = null;
    float defaultGravity = 0f;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
        defaultGravity = myRigidBody.gravityScale;
    }

    void Update()
    {
        Run();
        FlipSprite();
        Jump();
        ClimbLadder();
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float moveSpeed = controlThrow * runSpeed;
        Vector2 playerVelocity = new Vector2(moveSpeed, myRigidBody.velocity.y);
        
        myRigidBody.velocity = playerVelocity;

        myAnimator.SetBool("Running", IsPlayerMovingHorizontaly());
    }

    private void Jump()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                Vector2 jumpVelcityToAdd = new Vector2(0.0f, jumpSpeed);

                myRigidBody.velocity += jumpVelcityToAdd;
            }
        }
    }

    private void ClimbLadder()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("Climbing", IsPlayerMovingVertically());

            float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
            float moveSpeed = controlThrow * climbSpeed;
            Vector2 playerVelocity = new Vector2(myRigidBody.velocity.x, moveSpeed);

            myRigidBody.velocity = playerVelocity;
            myRigidBody.gravityScale = 0f;
        }
        else
        {
            myRigidBody.gravityScale = defaultGravity;
            myAnimator.SetBool("Climbing", false);
        }
    }

    private void FlipSprite()
    {
        if (IsPlayerMovingHorizontaly())
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    private bool IsPlayerMovingHorizontaly()
    {
        return Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
    }

    private bool IsPlayerMovingVertically()
    {
        return Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
    }
}
