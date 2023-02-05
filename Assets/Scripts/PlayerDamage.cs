using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    
    PlayerMovement playerMovement;

    private void Start() {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }


    // for player damage
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Goblin"){
            if (gameObject.name == "Kid") {
                GameSession.instance.RestartLevel();
            } else {
                playerMovement.gameObject.GetComponent<Rigidbody2D>().velocity += Vector2.up * 4f;
                playerMovement.BecomeChild();
            }
        }
        if (other.gameObject.tag == "Demon") {
            GameSession.instance.RestartLevel();
        }
    }





}
