using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSession : MonoBehaviour
{

    PlayerMovement playerMovement;
    int lives = 3;
    private bool isKid = true;
    public bool IsKid { get { return isKid; } set { isKid = value; } }

   private void Awake() {
    int gamesessions = FindObjectsOfType<GameSession>().Length;

    if (gamesessions > 1) {
        Destroy(gameObject);
    } else {
        DontDestroyOnLoad(gameObject);
    }

   }

   private void Start() {
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
