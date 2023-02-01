using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : MonoBehaviour
{
    
    
    Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            PlayerMovement player = other.GetComponentInParent<PlayerMovement>();

            if (player != null) {
                animator.Play("ShrinkPickedUp");
                player.Shrink();
            }
        }
    }

    public void DestroyGrowth() {
        Destroy(gameObject);
    }
}
