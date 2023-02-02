using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSession : MonoBehaviour
{

    int lives = 3;
    public bool isKid = true;

   private void Awake() {
    int gamesessions = FindObjectsOfType<GameSession>().Length;

    print(isKid + "gamessession isKid out of loop");
    if (gamesessions > 1) {
        Destroy(gameObject);
    } else {
        DontDestroyOnLoad(gameObject);
        print(isKid + "gamessession isKid");
    }

   }

    public void ProcessLevel() {
        StartCoroutine(nameof(NextLevel));
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
