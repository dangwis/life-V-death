using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeHUD : MonoBehaviour {

	public Slider healthSlider;

	public bool ______________________;

	public LifePlayer player;

	// Use this for initialization
	void Start () {
		healthSlider.maxValue = player.health;
	}
	
	// Update is called once per frame
	void Update () {
		healthSlider.value = player.health;
	}
}
