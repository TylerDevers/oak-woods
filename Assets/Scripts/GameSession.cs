using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSession : MonoBehaviour
{

    public static GameSession instance;
    int lives = 3;
    private bool isKid = true;
    public bool IsKid 
    { 
        get { 
            return isKid; 
        } 
        set { 
            isKid = value; 
        } 
    }


   private void Awake() {

    if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

   }


    public void RestartLevel() {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevel);
    }
  
   

}
