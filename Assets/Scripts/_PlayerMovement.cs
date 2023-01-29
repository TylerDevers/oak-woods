using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PlayerMovement : MonoBehaviour
{

    public Vector2 inputAxis;
    float moveSpeed = 50f, jumpForce = 200f;
    bool jumpPressed, isGrounded, swingingSword;
    new Rigidbody2D rigidbody;
    Animator animator;
    SpriteRenderer spriteRenderer;
    BoxCollider2D sword;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        sword = GameObject.Find("Sword").GetComponent<BoxCollider2D>();
    }

    private void Update() {
        inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetButtonDown("Jump")) {
            jumpPressed = true;
        }
        if (Input.GetButtonUp("Jump")) {
            jumpPressed = false;
        }

        AnimatePlayer();
        SwingSword();
    }

    private void FixedUpdate() {

        HorizontalMovement();
        if (jumpPressed) { Jump(); } 
        // if (!jumpPressed) { CutJumpShort(); }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            isGrounded = true;
        }
    }

    void AnimatePlayer() {
        if (inputAxis == Vector2.zero) {
            animator.SetBool("Run", false);
        }
        if (inputAxis.x > 0 ) {
            animator.SetBool("Run", true);
            spriteRenderer.flipX = false;
        }
        if (inputAxis.x < 0 ) {
            animator.SetBool("Run", true);
            spriteRenderer.flipX  = true;
        }
        if (jumpPressed && !isGrounded ) {
            animator.SetTrigger("Jump");
        }
    }

    
    void SwingSword() {
        if (Input.GetButtonDown("Fire1")) {
            sword.enabled = true;
            swingingSword = true;
            animator.SetTrigger("Attack");
        }
    }


    public void DisableSword() {
        sword.GetComponent<BoxCollider2D>().enabled = false;
        swingingSword = false;
    }


    void HorizontalMovement() {
        if (swingingSword) { return; }

        rigidbody.velocity = new Vector2(inputAxis.x * moveSpeed * Time.deltaTime, rigidbody.velocity.y);

    }

    void Jump() {
        if (swingingSword) { return; }

    
        if (jumpPressed && isGrounded) {
            rigidbody.velocity = new Vector2(inputAxis.x * moveSpeed, jumpForce) * Time.deltaTime;
            isGrounded = false;
        } 
    }

    


}

    

