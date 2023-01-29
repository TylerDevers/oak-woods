using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PlayerAnimation : MonoBehaviour
{
    
    Animator animator;
    PlayerMovement player;
    SpriteRenderer spriteRenderer;

    private void Start() {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }










}
