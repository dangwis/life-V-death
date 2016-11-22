﻿using UnityEngine;
using System.Collections;

public class FogOfWar : MonoBehaviour {
	public static int xVal = 8, yVal = 8;
	public static bool[,] fog = new bool[xVal,yVal];

	void Start() {
		for (int i = 0; i < xVal; i++) {
			for (int j = 0; j < yVal; j++) {
				//fog [i, j] = true;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		GameObject p1 = GameObject.Find ("Player 1"), p2 = GameObject.Find ("Player 2"), p3 = GameObject.Find ("Player 3");
		int x = 0, y = 0;
		if (p1 != null) {
			x = (int)(p1.transform.position.x / 74 * (xVal));
			y = (int)((- p1.transform.position.z / 74) * (yVal));

			fog [x, y] = true;
		}
		if (p2 != null) {
			x = (int)(p2.transform.position.x / 74 * xVal);
			y = (int)((- p2.transform.position.z / 74) * yVal);

			fog [x, y] = true;
		}
		if (p3 != null) {
			x = (int)(p3.transform.position.x / 74 * xVal);
			y = (int)((- p3.transform.position.z / 74) * yVal);

			fog [x, y] = true;
		}
	}
}
