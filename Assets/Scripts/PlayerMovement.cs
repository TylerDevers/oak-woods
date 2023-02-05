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

    private void Start()
    {
        PlayerState();
    }

    private void PlayerState()
    {
        print($"start, isKid is {GameSession.instance.IsKid}");
        if (GameSession.instance.IsKid)
        {
            adult.SetActive(false);
            kid.SetActive(true);
            jumpForce = 175f;
        }
        else if (!GameSession.instance.IsKid)
        {
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
            // gameSession.IsKid = false;
            GameSession.instance.IsKid = false;
            print("inside growth: isKid = " + GameSession.instance.IsKid);
        }
    }

    public void Shrink() {

        if (adult.activeSelf) { 
            rigidbody.isKinematic = true;
            adult.SetActive(false);
            kid.SetActive(true);
            jumpForce = 175f;
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            GameSession.instance.IsKid = true;
            print("inside growth: isKid = " + GameSession.instance.IsKid);
        }
    }


}
