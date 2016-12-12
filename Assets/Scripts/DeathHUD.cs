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
    public Image[] abilitiesImage;
    public GameObject deathWins, lifeWins;
    public GameObject fountainText, killPlayersText, useNumKeysText, scrollText, tabText, goText;
    public GameObject countdownToStart;
    public GameObject popupMenu;

	private int selectedAbility = -1;

	// Use this for initialization
	void Start () {
		Invoke ("FixRatio", 1f);
		Invoke ("fixOne", 0.2f);
		inst = this;
        switch (MapGenerator.fountLoc) {
            case 0:
                transform.Find("Panel").transform.Find("Map").GetComponent<Image>().sprite = minimapBL;
                break;
            case 1:
                transform.Find("Panel").transform.Find("Map").GetComponent<Image>().sprite = minimapBL;
                break;
            case 2:
                transform.Find("Panel").transform.Find("Map").GetComponent<Image>().sprite = minimapBL;
                break;
            case 3:
                transform.Find("Panel").transform.Find("Map").GetComponent<Image>().sprite = minimapBL;
                break;
        }
        countdownToStart.SetActive(true);
        lifeWins.SetActive(false);
        deathWins.SetActive(false);
        killPlayersText.SetActive(false);
        fountainText.SetActive(true);

        abilitiesImage = new Image[abilityImages.Length];
        for (int i = 0; i < abilityImages.Length; ++i) {
            abilitiesImage[i] = abilityImages[i].GetComponent<Image>();
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
        RectTransform tracker = transform.Find("Panel").transform.Find("Map").transform.Find ("Tracker Death").GetComponent<RectTransform> ();
        if (tracker != null) {
            Rect temp = GetComponent<RectTransform>().rect;
            float scaler = tracker.rect.height / temp.height;
            Vector3 vec = tracker.localScale;
            vec.y *= 0.07f / scaler;
            vec.x = GameObject.Find("Camera 0").GetComponent<Camera>().aspect * vec.y;
            tracker.localScale = vec;
            //tracker.localScale.x;
            //GetComponent<AspectRatioFitter>().as;
        }
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
            abilitiesImage[0].color = new Color(1f, 1f, 1f, 1f);
        } else {
            abilitiesImage[0].color = new Color(0.3f, 0.3f, 0.3f, 1f);
        }
        // Skeleton
        if (Death.S.manaLeft >= 35 && Death.S.curBigEn < Death.S.totalBigEnemyAllowed) {
            abilitiesImage[1].color = new Color(1f, 1f, 1f, 1f);
        } else {
            abilitiesImage[1].color = new Color(0.3f, 0.3f, 0.3f, 1f);
        }
        // Minotaur
        if (Death.S.manaLeft >= 40 && Death.S.curBigEn < Death.S.totalBigEnemyAllowed) {
            abilitiesImage[2].color = new Color(1f, 1f, 1f, 1f);
        } else {
            abilitiesImage[2].color = new Color(0.3f, 0.3f, 0.3f, 1f);
        }
        // Damange
        if (Death.S.manaLeft >= 25f && Death.S.curTrap < Death.S.totalTrapAllowed) {
            abilitiesImage[3].color = new Color(1f, 1f, 1f, 1f);
        } else {
            abilitiesImage[3].color = new Color(0.3f, 0.3f, 0.3f, 1f);
        }
        // Teleport
        if (Death.S.manaLeft >= 40 && Death.S.curTrap < Death.S.totalTrapAllowed) {
            abilitiesImage[4].color = new Color(1f, 1f, 1f, 1f);
        } else {
            abilitiesImage[4].color = new Color(0.3f, 0.3f, 0.3f, 1f);
        }
        // Mushroom
        if (Death.S.manaLeft >= 30 && Death.S.curTrap < Death.S.totalTrapAllowed) {
            abilitiesImage[5].color = new Color(1f, 1f, 1f, 1f);
        } else {
            abilitiesImage[5].color = new Color(0.3f, 0.3f, 0.3f, 1f);
        }

        if (Input.GetKey(KeyCode.Tab)) {
            popupMenu.SetActive(true);
        } else {
            popupMenu.SetActive(false);
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
