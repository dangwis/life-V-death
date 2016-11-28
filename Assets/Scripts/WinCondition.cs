using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour {
    public static int NumLivingPlayers = 0;
    public static bool FountainFound = false;
    public static bool deathWon = false;
    public static bool lifeWon = false;
    bool gameOver = false;

    // Use this for initialization
    void Awake() {
        deathWon = false;
        lifeWon = false;
    }

    // Update is called once per frame
    void Update() {
        if (gameOver) return;

        if (NumLivingPlayers == 0) {
            gameOver = true;
            deathWon = true;
            Debug.Log("Death Wins!");
            Invoke("RestartGame", 20f);
        }
        if (FountainFound) {
            gameOver = true;
            lifeWon = true;
            Debug.Log("Life Wins!");
            Invoke("RestartGame", 20f);
        }
    }

    void RestartGame() {
        SceneManager.LoadScene("Game_Setup");
    }
}