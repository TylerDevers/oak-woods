using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    
    // TODO : Load next level.
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            print(other.name);
            StartCoroutine(nameof(NextLevel));
        }
    }

    IEnumerator NextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        yield return new WaitForSeconds(1f);
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1) {
            SceneManager.LoadScene(currentSceneIndex + 1);
        } else {
            SceneManager.LoadScene(0);
        }
    }
}
