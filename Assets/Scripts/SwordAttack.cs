using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    
    Vector2 reversePosition, originalPosition;
    SpriteRenderer playerSprite;
    PlayerAnimation playerAnimation;
    BoxCollider2D swordCollider;

    private void Start() {
        originalPosition = transform.localPosition;
        reversePosition = new Vector2(-originalPosition.x, originalPosition.y);
        playerSprite = GetComponentInParent<SpriteRenderer>();
        playerAnimation = FindObjectOfType<PlayerAnimation>();
        swordCollider = GetComponentInChildren<BoxCollider2D>();
    }

    private void Update() {

        TriggerSwordCollider();
        // move sword hitbox
        if (playerSprite.flipX == false) {
            transform.localPosition = originalPosition;
        } else {
            transform.localPosition = reversePosition;
        }
    }

    void TriggerSwordCollider() {
        if (playerAnimation.isAttacking) {
            swordCollider.enabled = true;
        } else if (playerAnimation.isAttacking == false) {
            swordCollider.enabled = false;

        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        print("headbutt " + LayerMask.LayerToName(other.gameObject.layer));
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            other.gameObject.GetComponent<Demon>().RunAway();
            print("Sword hit");
        }
    }







}
