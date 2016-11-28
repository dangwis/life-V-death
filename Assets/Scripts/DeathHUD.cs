using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeathHUD : MonoBehaviour {
	public static DeathHUD inst;

	public Slider manaSlider;
	public GameObject manaCostSlider;
    public Sprite minimapUL, minimapUR, minimapBL, minimapBR;
    public GameObject[] selectionImages;
    public GameObject[] abilityImages;
    public GameObject deathWins, lifeWins;

	private int selectedAbility = -1;

	// Use this for initialization
	void Start () {
		Invoke ("FixRatio", 1f);
		Invoke ("fixOne", 0.2f);
		inst = this;
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

	public void fixOne() {
		manaSlider.maxValue = Death.S.totalMana;
		manaCostSlider.GetComponent<Slider>().maxValue = Death.S.totalMana;
	}


	public void FixRatio() {
		transform.Find ("Panel").transform.Find ("Map").GetComponent<AspectRatioFitter> ().aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
		transform.Find ("Panel").transform.Find ("Map").GetComponent<AspectRatioFitter> ().aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
		transform.Find ("Panel").transform.Find ("Map").GetComponent<AspectRatioFitter> ().aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
	}
	
	// Update is called once per frame
	void Update () {
		manaSlider.value = Death.S.manaLeft;
		if (selectedAbility != -1) {
			manaCostSlider.GetComponent<Slider> ().value = manaSlider.value - Death.S.manaCosts [selectedAbility];
		} else {
            manaCostSlider.GetComponent<Slider>().value = manaSlider.value;
        }

        if (WinCondition.lifeWon) {
            lifeWins.SetActive(true);
        }
        if (WinCondition.deathWon) {
            deathWins.SetActive(true);
        }

        // Grunt
        if (Death.S.manaLeft >= 50f && Death.S.curSpawner < Death.S.totalSpawnerAllowed) {
            abilityImages[0].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        } else {
            abilityImages[0].GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f, 1f);
        }
        // Skeleton
        if (Death.S.manaLeft >= 35 && Death.S.curBigEn < Death.S.totalBigEnemyAllowed) {
            abilityImages[1].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        } else {
            abilityImages[1].GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f, 1f);
        }
        // Minotaur
        if (Death.S.manaLeft >= 40 && Death.S.curBigEn < Death.S.totalBigEnemyAllowed) {
            abilityImages[2].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        } else {
            abilityImages[2].GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f, 1f);
        }
        // Damange
        if (Death.S.manaLeft >= 25f && Death.S.curTrap < Death.S.totalTrapAllowed) {
            abilityImages[3].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        } else {
            abilityImages[3].GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f, 1f);
        }
        // Teleport
        if (Death.S.manaLeft >= 40 && Death.S.curTrap < Death.S.totalTrapAllowed) {
            abilityImages[4].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        } else {
            abilityImages[4].GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f, 1f);
        }
        // Mushroom
        if (Death.S.manaLeft >= 30 && Death.S.curTrap < Death.S.totalTrapAllowed) {
            abilityImages[5].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        } else {
            abilityImages[5].GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f, 1f);
        }
    }

	public void selectAbility(int num) {
		deselectAllAbilities ();
		selectionImages [num - 1].SetActive(true);

		//display mana cost for ability
		selectedAbility = num - 1;
	}

	public void deselectAllAbilities() {
		foreach (GameObject I in selectionImages) {
			I.SetActive(false);
		}
		selectedAbility = -1;
	}
}
