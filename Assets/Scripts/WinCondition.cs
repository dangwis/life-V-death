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
    public int totalReached;

    // Use this for initialization
    void Awake() {
        S = this;
        deathWon = false;
        lifeWon = false;
        totalReached = 0;
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
        if(foundFountain[life.playerNum - 1] == false)
        {
            foundFountain[life.playerNum - 1] = true;
            totalReached++;
            life.immortal = true;
            if(totalReached == SetupCameras.PlayerCount - 1)
            {
                gameOver = true;
                lifeWon = true;
                Debug.Log("Life Wins!");
                Invoke("RestartGame", 20f);
            }
            else
            {
                life.ShowPlayerImmortal();
            }
            
        }
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