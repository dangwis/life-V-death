using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeathHUD : MonoBehaviour {
	public static DeathHUD inst;

	public Slider manaSlider;
	public GameObject[] selectionImages;

	// Use this for initialization
	void Start () {
		Invoke ("FixRatio", 1f);
		Invoke ("fixOne", 0.2f);
		inst = this;
	}

	public void fixOne() {
		manaSlider.maxValue = Death.S.totalMana;
	}


	public void FixRatio() {
		transform.Find ("Panel").transform.Find ("Map").GetComponent<AspectRatioFitter> ().aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
		transform.Find ("Panel").transform.Find ("Map").GetComponent<AspectRatioFitter> ().aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
		transform.Find ("Panel").transform.Find ("Map").GetComponent<AspectRatioFitter> ().aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
	}
	
	// Update is called once per frame
	void Update () {
		manaSlider.value = Death.S.manaLeft;

	}

	public void selectAbility(int num) {
		deselectAllAbilities ();
		selectionImages [num - 1].SetActive(true);
	}

	public void deselectAllAbilities() {
		foreach (GameObject I in selectionImages) {
			I.SetActive(false);
		}
	}
}
