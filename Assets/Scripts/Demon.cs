using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{

    new Rigidbody2D rigidbody;
    Transform player;
    CapsuleCollider2D cowardsBody;
    BoxCollider2D braveBody;
    Animator animator;
    private bool playerClose, runAway, knockedBack;
    float distance, awayFromPlayer;
    float speed = 40f;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        braveBody = GetComponent<BoxCollider2D>();
        cowardsBody = GetComponent<CapsuleCollider2D>();
        player = GameObject.Find("Player").transform;
         
    }


    private void Update() {

        // Enemy looks towards player position
        if (!runAway) {
            if (player.position.x < transform.position.x) {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }else {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        // player within distance, enemy attacks
        distance = Vector2.Distance(player.position, transform.position);
        if (distance < 1f) {
            playerClose = true;
        } else {
            playerClose = false;
        }

    }

    void FixedUpdate()
    {
        Attack();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Rock" ) {
            RunAway();
        }
    }
    void Attack() {

        if (knockedBack) { return; }

        if (runAway) {
            rigidbody.velocity = new Vector2( awayFromPlayer * speed * Time.fixedDeltaTime, rigidbody.velocity.y);
            animator.Play("DemonDefeat");  
        } else if (playerClose) {
            animator.Play("DemonRun");
            rigidbody.position = Vector2.MoveTowards(transform.position, player.position, 0.1f * Time.fixedDeltaTime);
        } else {
            rigidbody.position = rigidbody.position;
            animator.Play("DemonIdle");
        }

    }

    public void RunAway() {
        awayFromPlayer = -Mathf.Sign(player.position.x - transform.position.x);
        
        braveBody.enabled = false;
        runAway = true;
        rigidbody.isKinematic = true;

        transform.position = new Vector2(transform.position.x, -0.369f); // correct coward sprites position.
        if (gameObject.GetComponent<SpriteRenderer>().flipX == false) {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        } else {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

    }

    public void TriggerKnockedBack() {
        StartCoroutine(nameof(KnockBack));
    }

    IEnumerator KnockBack() {
        float direction = Mathf.Sign(transform.position.x - player.position.x);
        knockedBack = true;
        rigidbody.velocity = new Vector2(direction * 2f, rigidbody.velocity.y);

        yield return new WaitForSeconds(2f);
        knockedBack = false;

    }

    void OnBecameInvisible()  {
        if (!runAway) { return; }

        Destroy(gameObject);
        
    }

}
