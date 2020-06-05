using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    [Tooltip("Multiplier for player run speed")]
    [SerializeField] float runSpeed = 5f;

    Rigidbody2D myRigidBody = null;
    Animator myAnimator = null;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        FlipSprite();
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float moveSpeed = controlThrow * runSpeed;
        Vector2 playerVelocity = new Vector2(moveSpeed, myRigidBody.velocity.y);
        
        myRigidBody.velocity = playerVelocity;

        bool playerIsMoving = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        myAnimator.SetBool("Running", playerIsMoving);
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalMovement = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalMovement)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }
}
