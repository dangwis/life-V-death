using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour {
    public static WinCondition S;
    public static bool deathWon = false;
    public static bool lifeWon = false;
    public static List<bool> foundFountain = new List<bool>();
    bool gameOver = false;

    // Use this for initialization
    void Awake() {
        S = this;
        deathWon = false;
        lifeWon = false;
    }

    public static void CreateList(int numPlayers)
    {
        for(int i = 0; i < numPlayers - 1; i++)
        {
            bool temp = false;
            foundFountain.Add(temp);
        }
    }

    public void UpdateWinCondition(LifePlayer life)
    {
        foundFountain[life.playerNum] = true;
        life.immortal = true;
        for (int i = 0; i < foundFountain.Count; i++)
        {
            if (foundFountain[i] == false) return;
        }
        gameOver = true;
        lifeWon = true;
        Debug.Log("Life Wins!");
        Invoke("RestartGame", 20f);
    }

    public void DeathWins()
    {
        gameOver = true;
        deathWon = true;
        Debug.Log("Death Wins!");
        Invoke("RestartGame", 20f);
    }

    void RestartGame() {
        SceneManager.LoadScene("Game_Setup");
    }
}