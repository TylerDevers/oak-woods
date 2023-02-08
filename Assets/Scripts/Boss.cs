using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    
    new Rigidbody2D rigidbody;
    new Collider2D collider;
    SpriteRenderer spriteRenderer;
    Transform player;
    Animator animator;
    private bool playerClose, dead;
    float distance, awayFromPlayer;
    [SerializeField] float runSpeed = 0.5f, attackDistance = 2f;
    Color bossStartingColor;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").transform;
        bossStartingColor = spriteRenderer.color;
         
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
        if (dead) { return; }

        if (playerClose) {
            animator.Play("Run");
            rigidbody.position = Vector2.MoveTowards(transform.position, player.position, runSpeed * Time.fixedDeltaTime);
        } else {
            rigidbody.position = rigidbody.position;
            animator.Play("Idle");
        }

    }


    private void OnCollisionEnter2D(Collision2D other) {
         if (other.collider.tag == "Rock") {
            Damage(other.collider.tag);
            Debug.Log("Rock");
        }

        if (other.collider.tag == "Headbutt") {
            Damage(other.collider.tag);
        } else if (other.collider.tag == "Sword") {
            Damage(other.collider.tag);
        } else if (other.collider.tag == "Player") {
            if (!GameSession.instance.IsKid) {


            } 
        }
    }

    void Damage(string hitBy) {
        float knockBackDirection = -Mathf.Sign(player.position.x - transform.position.x);
        if (hitBy == "Rock") {
            dead = true;
            animator.Play("Death");
            spriteRenderer.color = Color.red;
            rigidbody.constraints = RigidbodyConstraints2D.None;
            collider.enabled = false;
            // rigidbody.velocity += new Vector2 (knockBackDirection, 2f);
        } else if (hitBy == "Headbutt") {
            rigidbody.velocity += new Vector2 (knockBackDirection, 2f);
        } else if (hitBy == "Sword") {
            rigidbody.velocity += new Vector2 (knockBackDirection, 2f);
            StartCoroutine(nameof(ChangeColor));
        }
        
    }

    IEnumerator ChangeColor() {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = bossStartingColor;

    }
    void OnBecameInvisible()  {
        if (!dead) { return; }

        Destroy(gameObject);
    }
        
}
