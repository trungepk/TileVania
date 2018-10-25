using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpPower = 5f;
    [SerializeField] float climbSpeed = 5f;
    Rigidbody2D myRigidbody;
    SpriteRenderer playerSprite;
    Animator animator;
    Collider2D myCollider;
    float gravityScaleAtStart;

    void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
	}
	
	void Update () {
        Run();
        Jump();
        ClimbLadder();
	}

    private void Run()
    {
        var controlThrow = CrossPlatformInputManager.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        Vector2 runningVelocity = new Vector2(controlThrow, myRigidbody.velocity.y);
        myRigidbody.velocity = runningVelocity;
        FlipSprite();
        TransitionToRunningAnimation();
    }

    private void FlipSprite()
    {
        if (myRigidbody.velocity.x > 0)
            playerSprite.flipX = false;
        else if(myRigidbody.velocity.x < 0)
            playerSprite.flipX = true;
    }

    private void TransitionToRunningAnimation()
    {
        var IsPlayerRunning = myRigidbody.velocity.x != 0;
        animator.SetBool("Running", IsPlayerRunning);
    }

    private void Jump()
    {
        var IsTouchingGround = myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && myRigidbody.velocity.y == 0;
        if (CrossPlatformInputManager.GetButtonDown("Jump") && IsTouchingGround)
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpPower);
            myRigidbody.velocity += jumpVelocityToAdd;
        }
    }

    private void ClimbLadder()
    {
        if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            animator.SetBool("Climbing", false);
            myRigidbody.gravityScale = gravityScaleAtStart;
            return;
        }
        var controlThrow = CrossPlatformInputManager.GetAxis("Vertical") * climbSpeed * Time.deltaTime;
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, controlThrow);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;
        TransitionToClimbingAnimation();
    }

    private void TransitionToClimbingAnimation()
    {
        var IsPlayerClimbing = myRigidbody.velocity.y != 0;
        animator.SetBool("Climbing", IsPlayerClimbing);
    }
}
