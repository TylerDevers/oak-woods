using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    PlayerMovement player;
    SpriteRenderer spriteRenderer;
    public bool isAttacking;

    private void Start() {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        AnimatePlayer();
        Attack();
    }

    void AnimatePlayer() {

        if (isAttacking) { return; }

        if (player.inputAxis == Vector2.zero && player.isGrounded) {
            // animator.SetBool("Run", false);
            animator.Play("Idle");
        }
        if (player.inputAxis.x > 0  && player.isGrounded) {
            // animator.SetBool("Run", true);
            animator.Play("Run");
            spriteRenderer.flipX = false;
        }
        if (player.inputAxis.x < 0  && player.isGrounded) {
            // animator.SetBool("Run", true);
            animator.Play("Run");
            spriteRenderer.flipX  = true;
        }
        if (player.isGrounded == false ) {
            animator.Play("Jump");
        }
    }


    void Attack() {
        if (Input.GetButtonDown("Fire1") && !isAttacking) {
            isAttacking = true;
            animator.Play("Attack");
        }
    }

    public void AttackFinished() {
        isAttacking = false;
    }


// if (Input.GetButtonDown("Fire1")) {
//             sword.enabled = true;
//             swingingSword = true;
//             animator.SetTrigger("Attack");
//         }



}

