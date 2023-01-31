using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    
    // TODO : Load next level.
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            print(other.name);
        }
    }
}
