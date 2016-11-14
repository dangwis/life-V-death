using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeathHUD : MonoBehaviour {

	public Slider manaSlider;

	// Use this for initialization
	void Start () {
		manaSlider.maxValue = Death.S.totalMana;
	}
	
	// Update is called once per frame
	void Update () {
		manaSlider.value = Death.S.manaLeft;
	}
}
