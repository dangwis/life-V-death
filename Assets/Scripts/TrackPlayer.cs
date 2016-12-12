﻿using UnityEngine;
using System.Collections;

public class TrackPlayer : MonoBehaviour {
	public string trackName = "";
	GameObject trackObj;
	bool setup = false;
	RectTransform trans;
	public GameObject map;

	// Use this for initialization
	void Start () {
		Invoke ("Setup", 0.02f);
	}

	void Setup() {
		trackObj = GameObject.Find (trackName);
		if (trackObj == null) {
			this.gameObject.SetActive (false);
		}

		trans = GetComponent<RectTransform> ();
		setup = true;
        if (map.GetComponent<FixRatio>() != null) {
            map.GetComponent<FixRatio>().Fix();
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (!setup)
			return;

		Vector3 vec = trackObj.transform.position;

		vec.x = vec.x / 85 * map.GetComponent<RectTransform>().rect.width;
		vec.y = map.GetComponent<RectTransform>().rect.height + vec.z / 85 * map.GetComponent<RectTransform>().rect.height;
		vec.z = 0;

		trans.transform.localPosition = vec;
	}
}
