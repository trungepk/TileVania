using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidbody;
    SpriteRenderer enemySprite;

	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();
	}
	
	void Update () {
        myRigidbody.velocity = new Vector2(moveSpeed, 0f);
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ChangeDirectionOnEdge();
    }

    private void ChangeDirectionOnEdge()
    {
        if (myRigidbody.velocity.x > 0)
        {
            enemySprite.flipX = true;
            moveSpeed *= -1;
        }
        else if (myRigidbody.velocity.x < 0)
        {
            enemySprite.flipX = false;
            moveSpeed *= -1;
        }
    }
}
