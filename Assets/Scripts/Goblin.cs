using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    new Rigidbody2D rigidbody;
    Transform player;
    Animator animator;
    private bool playerClose;
    float distance, awayFromPlayer;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
         
    }


    private void Update() {

        // Enemy looks towards player position
        if (player.position.x < transform.position.x) {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }else {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        // player within distance, enemy attacks
        distance = Vector2.Distance(player.position, transform.position);
        if (distance < 3f) {
            playerClose = true;
        } else {
            playerClose = false;
        }

    }

    void FixedUpdate()
    {
        Attack();
    }

    void Attack() {

        if (playerClose) {
            animator.Play("Run");
            rigidbody.position = Vector2.MoveTowards(transform.position, player.position, 0.1f * Time.deltaTime);
        } else {
            rigidbody.position = rigidbody.position;
            animator.Play("Idle");
        }

    }



    void OnBecameInvisible()  {

        Destroy(gameObject);
    }
        
}
