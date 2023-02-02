using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
   private void Awake() {
    int gamesessions = GetComponents<GameSession>().Length;

    if (gamesessions > 0) {
        Destroy(gameObject);
    } else {
        DontDestroyOnLoad(gameObject);
    }
    


   }

}
