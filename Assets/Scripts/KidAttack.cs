using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidAttack : MonoBehaviour
{
    Vector2 reversePosition, originalPosition;
    SpriteRenderer playerSprite;
    PlayerAnimation playerAnimation;
    BoxCollider2D attackCollider;

    private void Start() {
        originalPosition = transform.localPosition;
        reversePosition = new Vector2(-originalPosition.x, originalPosition.y);
        playerSprite = GetComponentInParent<SpriteRenderer>();
        playerAnimation = FindObjectOfType<PlayerAnimation>();
        attackCollider = GetComponentInChildren<BoxCollider2D>();
        attackCollider.enabled = false;
    }

    private void Update() {

        TriggerAttackCollider();
        // move headbutt collider
        if (playerSprite.flipX == false) {
            transform.localPosition = originalPosition;
        } else {
            transform.localPosition = reversePosition;
        }
    }

    void TriggerAttackCollider() {
        if (playerAnimation.isAttacking) {
            attackCollider.enabled = true;
        } else if (playerAnimation.isAttacking == false) {
            attackCollider.enabled = false;

        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Demon") {
            other.gameObject.GetComponent<Demon>().TriggerKnockedBack();
        }
    }

}
