using UnityEngine;
using System.Collections;

public class WinCondition : MonoBehaviour {
    public static int NumLivingPlayers = 0;
    public static bool FountainFound = false;
    bool gameOver = false;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (gameOver) return;

        if (NumLivingPlayers == 0) {
            gameOver = true;
            Debug.Log("Death Wins!");
        }
        if (FountainFound) {
            gameOver = true;
            Debug.Log("Life Wins!");
        }
    }
}