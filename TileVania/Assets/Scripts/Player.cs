using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {
    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D myRigidbody;
    SpriteRenderer playerSprite;
    Animator animator;

	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
	}
	
	void Update () {
        Run();
	}

    private void Run()
    {
        var controlThrow = CrossPlatformInputManager.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        Vector2 playerVelocity = new Vector2(controlThrow, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        FlipSprite();
        TransitionToRunningAnimation();
    }

    private void TransitionToRunningAnimation()
    {
        bool isPlayerRunning = myRigidbody.velocity.x != 0;
        animator.SetBool("Running", isPlayerRunning);
    }

    private void FlipSprite()
    {
        if (myRigidbody.velocity.x > 0)
        {
            playerSprite.flipX = false;
        }
        else if(myRigidbody.velocity.x < 0)
        {
            playerSprite.flipX = true;
        }
    }
}
