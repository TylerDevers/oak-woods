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
    GameSession gameSession;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        playerAnimation = FindObjectOfType<PlayerAnimation>();
        kid = GameObject.Find("Kid");
        gameSession = FindObjectOfType<GameSession>();
    }

    private void Start() {
        if (gameSession.isKid) {
            print(gameSession.isKid + " isKid");
            adult.SetActive(false);
            kid.SetActive(true);
            jumpForce = 150f;
        } else {
            kid.SetActive(false);
            adult.SetActive(true);
            jumpForce = 200f;
        }
    }

    private void Update() {
        inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetButtonDown("Jump") && !jumpPressed) {
            jumpPressed = true;
        }

    }

    private void FixedUpdate() {

        if (playerAnimation.isAttacking) { return; }

        HorizontalMovement();
        if (jumpPressed) { Jump(); } 
        
    }

    // for feet
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            isGrounded = true;
            jumpPressed = false;
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
            gameSession.isKid = false;
        }
    }

    public void Shrink() {

        if (adult.activeSelf) { 
            rigidbody.isKinematic = true;
            adult.SetActive(false);
            kid.SetActive(true);
            jumpForce = 150f;
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            gameSession.isKid = true;
        }
    }


}
