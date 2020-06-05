using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    [Tooltip("Multiplier for player run speed")]
    [SerializeField] float runSpeed = 5f;
    [Tooltip("Amount to add to player's Y velocity when jumping")]
    [SerializeField] float jumpSpeed = 5f;

    Rigidbody2D myRigidBody = null;
    Animator myAnimator = null;
    Collider2D myCollider = null;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        Run();
        FlipSprite();
        Jump();
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
}
