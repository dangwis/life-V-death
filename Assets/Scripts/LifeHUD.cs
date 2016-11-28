using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeHUD : MonoBehaviour {

	public Slider healthSlider;
    public Sprite minimapUL, minimapUR, minimapBL, minimapBR;
    public GameObject deathWins, lifeWins;

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
    }
}
