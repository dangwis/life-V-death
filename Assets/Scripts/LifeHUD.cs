using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeHUD : MonoBehaviour {

	public Slider healthSlider;
    public Sprite minimapUL, minimapUR, minimapBL, minimapBR;
    public GameObject deathWins, lifeWins, gameCountdown, goText;

    public bool ______________________;

	public LifePlayer player;

	// Use this for initialization
	void Start () {
		healthSlider.maxValue = player.health;
        switch (MapGenerator.fountLoc) {
            case 0:
                transform.Find("Panel").transform.Find("Map").GetComponent<Image>().sprite = minimapUL;
                break;
            case 1:
                transform.Find("Panel").transform.Find("Map").GetComponent<Image>().sprite = minimapUR;
                break;
            case 2:
                transform.Find("Panel").transform.Find("Map").GetComponent<Image>().sprite = minimapBL;
                break;
            case 3:
                transform.Find("Panel").transform.Find("Map").GetComponent<Image>().sprite = minimapBR;
                break;
        }
        gameCountdown.SetActive(true);
        lifeWins.SetActive(false);
        deathWins.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		healthSlider.value = player.health;
        if (WinCondition.lifeWon) {
            lifeWins.SetActive(true);
        }
        if (WinCondition.deathWon) {
            deathWins.SetActive(true);
        }
        UpdateCountdown();
    }

    void UpdateCountdown()
    {
        if (GameManager.S.setupTime - (Time.time - GameManager.S.startTime) > 0)
        {
            gameCountdown.GetComponent<Text>().text = "Game Start: " + (int)(GameManager.S.setupTime - (Time.time - GameManager.S.startTime));
        }
        else
        {
            gameCountdown.SetActive(false);
            goText.SetActive(true);
        }
        if(GameManager.S.endGo - (Time.time - GameManager.S.startTime) < 0)
        {
            goText.SetActive(false);
        }
    }
}
