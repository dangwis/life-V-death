using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager S;

    public float setupTime;
    float startTime;
    public float showFountainTime, showLifeTime;
    public bool gameStart, showFountain, showLife;

	// Use this for initialization
	void Start () {
        S = this;
        startTime = Time.time;
        gameStart = false;
        showLife = false;
        showFountain = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - startTime > setupTime)
        {
            gameStart = true;
        }
        if(Time.time - startTime > showFountainTime)
        {
            showFountain = true;
        }
        if (Time.time - startTime > showLifeTime)
        {
            showLife = true;
        }
    }
}
