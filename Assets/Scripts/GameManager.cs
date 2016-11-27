using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager S;

    public float setupTime;
    float startTime;

    public bool gameStart;

	// Use this for initialization
	void Start () {
        S = this;
        startTime = Time.time;
        gameStart = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - startTime > setupTime)
        {
            gameStart = true;
        }
	}
}
