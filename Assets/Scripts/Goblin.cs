using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    new Rigidbody2D rigidbody;
    new Collider2D collider;
    Transform player;
    Animator animator;
    private bool playerClose, dead;
    float distance, awayFromPlayer;
    [SerializeField] float runSpeed = 0.2f, attackDistance = 2f;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
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
        if (distance < attackDistance) {
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
            rigidbody.position = Vector2.MoveTowards(transform.position, player.position, runSpeed * Time.fixedDeltaTime);
        } else {
            rigidbody.position = rigidbody.position;
            animator.Play("Idle");
        }

    }


    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.tag == "Headbutt") {
            Damage(other.collider.tag);
        } else if (other.collider.tag == "Sword") {
            Damage(other.collider.tag);
        } else if (other.collider.tag == "Player") {
            print(other.collider.name + " 3");
        }
    }

    void Damage(string hitBy) {
        float knockBackDirection = -Mathf.Sign(player.position.x - transform.position.x);
        rigidbody.velocity += new Vector2 (knockBackDirection, 2f);
        if (hitBy == "Sword") {
            rigidbody.isKinematic = true;
            rigidbody.constraints = RigidbodyConstraints2D.None;
        }
        collider.enabled = false;
        GetComponent<SpriteRenderer>().sortingOrder = 20;
        dead = true;

    }
    void OnBecameInvisible()  {
        if (!dead) { return; }

        Destroy(gameObject);
    }
        
}
