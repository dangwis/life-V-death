using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour {
    public static int NumLivingPlayers = 0;
    public static bool FountainFound = false;
    public static bool deathWon = false;
    public static bool lifeWon = false;
    public static List<bool> foundFountain;
    bool gameOver = false;

    // Use this for initialization
    void Awake() {
        NumLivingPlayers = 0;
        FountainFound = false;
        deathWon = false;
        lifeWon = false;
    }

    public static void CreateList(int numPlayers)
    {
        foundFountain = new List<bool>(numPlayers);
        for(int i = 0; i < numPlayers; i++)
        {
            foundFountain[i] = false;
        }
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