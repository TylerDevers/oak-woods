using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Vector2 inputAxis; 
    Vector2 reversePosition, originalPosition;
    float moveSpeed = 50f, jumpForce = 150f;
    public bool jumpPressed, isGrounded;
    new Rigidbody2D rigidbody;
    PlayerAnimation playerAnimation;
    GameObject kid;
    [SerializeField] GameObject adult;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        playerAnimation = FindObjectOfType<PlayerAnimation>();
        kid = GameObject.Find("Kid");

    }


    private void Update() {
        inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetButtonDown("Jump")) {
            jumpPressed = true;
        }
        if (Input.GetButtonUp("Jump")) {
            jumpPressed = false;
        }


    }

    private void FixedUpdate() {

        if (playerAnimation.isAttacking) { return; }

        HorizontalMovement();
        if (jumpPressed) { Jump(); } 
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            isGrounded = true;
        }
    }

    void HorizontalMovement() {

        rigidbody.velocity = new Vector2(inputAxis.x * moveSpeed * Time.deltaTime, rigidbody.velocity.y);

    }

    void Jump() {
    
        if (jumpPressed && isGrounded) {
            rigidbody.velocity = new Vector2(inputAxis.x * moveSpeed, jumpForce) * Time.deltaTime;
            isGrounded = false;
        } 
    }

    public void Growth() {

        if (kid.activeSelf) { 
            rigidbody.isKinematic = true;
            kid.SetActive(false);
            adult.SetActive(true);
            jumpForce = 200f;
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            
        }
    }

   



}
