using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    
    GameSession gameSession;

    private void Awake() {
        gameSession = FindObjectOfType<GameSession>();
    }


    // TODO : Load next level.
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            gameSession.ProcessLevel();
        }
    }

}  