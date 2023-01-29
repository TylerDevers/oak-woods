using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growth : MonoBehaviour
{
    
    
    Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            animator.Play("GrowthPickedUp");
        }
    }

    public void DestroyGrowth() {
        Destroy(gameObject);
    }
}
